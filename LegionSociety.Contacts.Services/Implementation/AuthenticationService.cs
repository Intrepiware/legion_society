using LegionSociety.Contacts.Models;
using OtpNet;
using QRCoder;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Contact = LegionSociety.Contacts.Data.Models.Contact;


namespace LegionSociety.Contacts.Services.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IPasswordHashService PasswordHashService;
        private readonly IRepository<Contact> ContactRepository;
        const string TotpDescription = "Contacts Database";
        const string TotpIssuer = "Legion Society";

        public AuthenticationService(IPasswordHashService passwordHashService,
            IRepository<Contact> contactRepository)
        {
            this.PasswordHashService = passwordHashService;
            this.ContactRepository = contactRepository;
        }

        public async Task<QrResponseModel> CreateTotp(long contactId)
        {
            var contact = await ContactRepository.GetById(contactId);
            if (contact == null || contact.TotpConfirmDate.HasValue)
            {
                return null;
            }
            var secret = Base32Encoding.ToString(KeyGeneration.GenerateRandomKey(20));
            var qrCodeUri = $"otpauth://totp/{Uri.EscapeDataString(TotpDescription)}?secret={secret}&issuer={Uri.EscapeDataString(TotpIssuer)}";

            var qrGenerator = new QRCodeGenerator();
            using (var qrCodeData = qrGenerator.CreateQrCode(qrCodeUri, QRCodeGenerator.ECCLevel.Q))
            {
                var qrCode = new PngByteQRCode(qrCodeData);

                contact.TotpKey = secret;
                ContactRepository.Update(contact);

                return new QrResponseModel
                {
                    QrImage = qrCode.GetGraphic(20),
                    TotpCode = secret
                };
            }
        }

        public Contact Validate(string emailAddress, string password)
        {
            var contact = ContactRepository.GetAll().Where(x => x.EmailAddress == emailAddress).SingleOrDefault();
            var pwValid = PasswordHashService.Verify(password, contact?.Password ?? "dummy");

            if (!pwValid || contact == null)
                return null;

            return contact;
        }
    }
}
