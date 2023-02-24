using LegionSociety.Contacts.Data.Models;
using OtpNet;
using QRCoder;

namespace LegionSociety.Contacts.Services.Implementation
{
    public class MfaService : IMfaService
    {
        public IRepository<Contact> ContactRepository;
        const string TotpIssuer = "Legion Society";

        public MfaService(IRepository<Contact> contactRepository)
        {
            ContactRepository = contactRepository;
        }

        public string GenerateKey() => Base32Encoding.ToString(KeyGeneration.GenerateRandomKey(20));

        public byte[] GenerateQr(string key, string emailAddress)
        {
            var qrCodeUri = new OtpUri(OtpType.Totp, key, emailAddress, TotpIssuer).ToString();
            var qrGenerator = new QRCodeGenerator();
            using (var qrCodeData = qrGenerator.CreateQrCode(qrCodeUri, QRCodeGenerator.ECCLevel.Q))
            {
                var qrCode = new PngByteQRCode(qrCodeData);
                return qrCode.GetGraphic(20);
            }
        }

        public bool Verify(string key, string code)
        {
            var keyBytes = Base32Encoding.ToBytes(key);
            var totp = new Totp(keyBytes);
            return totp.VerifyTotp(code, out _, new VerificationWindow(1, 1));
        }
    }
}
