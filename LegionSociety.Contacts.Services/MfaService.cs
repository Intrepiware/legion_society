using LegionSociety.Contacts.Data.Models;
using LegionSociety.Contacts.Models;
using OtpNet;
using QRCoder;
using System;
using System.Threading.Tasks;

namespace LegionSociety.Contacts.Services
{
    public class MfaService : IMfaService
    {
        public IRepository<Contact> ContactRepository;
        const string TotpIssuer = "Legion Society";

        public MfaService(IRepository<Contact> contactRepository)
        {
            ContactRepository = contactRepository;
        }


        public async Task<QrResponseModel> Initialize(long contactId)
        {
            var contact = await ContactRepository.GetById(contactId);
            if (contact == null || contact.TotpConfirmDate.HasValue)
            {
                return null;
            }

            var secret = contact.TotpKey ?? Base32Encoding.ToString(KeyGeneration.GenerateRandomKey(20));
            var qrCodeUri = new OtpUri(OtpType.Totp, secret, contact.EmailAddress, TotpIssuer).ToString();
            var qrGenerator = new QRCodeGenerator();
            using (var qrCodeData = qrGenerator.CreateQrCode(qrCodeUri, QRCodeGenerator.ECCLevel.Q))
            {
                var qrCode = new PngByteQRCode(qrCodeData);

                contact.TotpKey = secret;
                await ContactRepository.Update(contact);

                return new QrResponseModel
                {
                    QrImage = qrCode.GetGraphic(20),
                    TotpCode = secret
                };
            }
        }

        public async Task<bool> Verify(long contactId, string code)
        {
            var contact = await ContactRepository.GetById(contactId);
            if (contact?.TotpKey == null)
                return false;

            var key = Base32Encoding.ToBytes(contact.TotpKey);
            var totp = new Totp(key);
            if(totp.VerifyTotp(code, out _, new VerificationWindow(1, 1)))
            {
                if (contact.TotpConfirmDate == null)
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
