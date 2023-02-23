using LegionSociety.Contacts.Models;
using System.Linq;
using Contact = LegionSociety.Contacts.Data.Models.Contact;


namespace LegionSociety.Contacts.Services.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IPasswordHashService PasswordHashService;
        private readonly IRepository<Contact> ContactRepository;

        public AuthenticationService(IPasswordHashService passwordHashService,
            IRepository<Contact> contactRepository)
        {
            this.PasswordHashService = passwordHashService;
            this.ContactRepository = contactRepository;
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
    }
}
