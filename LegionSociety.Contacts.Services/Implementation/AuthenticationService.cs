using LegionSociety.Contacts.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Contact = LegionSociety.Contacts.Data.Models.Contact;


namespace LegionSociety.Contacts.Services.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IPasswordHashService PasswordHashService;
        private readonly IRepository<Contact> ContactRepository;
        private readonly IMfaService MfaService;

        public AuthenticationService(IPasswordHashService passwordHashService,
            IRepository<Contact> contactRepository,
            IMfaService mfaService)
        {
            this.PasswordHashService = passwordHashService;
            this.ContactRepository = contactRepository;
            MfaService = mfaService;
        }

        public AuthenticationResultModel Validate(string emailAddress, string password)
        {
            var contact = ContactRepository.GetAll().Where(x => x.EmailAddress == emailAddress).SingleOrDefault();
            var pwValid = PasswordHashService.Verify(password, contact?.Password ?? "dummy");

            if (!pwValid || contact == null)
                return new AuthenticationResultModel { Result = AuthenticationResult.InvalidPassword };

            if (contact.TotpConfirmDate == null)
                return new AuthenticationResultModel { Result = AuthenticationResult.MfaRegistrationRequired, ContactId = contact.Id };
            else
                return new AuthenticationResultModel { Result = AuthenticationResult.MfaVerificationRequired, ContactId = contact.Id };
        }

        public async Task<InitializeMfaModel> InitializeMfa(long contactId)
        {
            var contact = await ContactRepository.GetById(contactId);
            if (contact == null || contact.TotpConfirmDate.HasValue)
            {
                return null;
            }

            var secret = contact.TotpKey;
            if(secret == null)
            {
                secret = MfaService.GenerateKey();
                contact.TotpKey = secret;
                await ContactRepository.Update(contact);
            }

            var qrBytes = MfaService.GenerateQr(secret, contact.EmailAddress);
            return new InitializeMfaModel
            {
                QrImage = qrBytes,
                TotpCode = secret
            };
        }

        public async Task<bool> VerifyMfa(long contactId, string code)
        {
            var contact = await ContactRepository.GetById(contactId);
            if (contact?.TotpKey == null)
                return false;

            if(MfaService.Verify(contact.TotpKey, code))
            {
                if(contact.TotpConfirmDate == null)
                {
                    contact.TotpConfirmDate = DateTime.UtcNow;
                    await ContactRepository.Update(contact);
                }
                return true;
            }

            return false;
        }
    }
}
