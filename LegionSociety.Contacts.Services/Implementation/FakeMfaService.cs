using QRCoder;

namespace LegionSociety.Contacts.Services.Implementation
{
    public class FakeMfaService : IMfaService
    {
        public string GenerateKey() => "FAKE-KEY";
        public byte[] GenerateQr(string key, string emailAddress)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrCodeUri = "https://www.youtube.com/watch?v=dQw4w9WgXcQ"; // Never gonna give you up!
            using (var qrCodeData = qrGenerator.CreateQrCode(qrCodeUri, QRCodeGenerator.ECCLevel.Q))
            {
                var qrCode = new PngByteQRCode(qrCodeData);
                return qrCode.GetGraphic(20);
            }
        }
        public bool Verify(string key, string code) => code == "123456";
    }
}
