namespace LegionSociety.Contacts.Models
{
    public class AuthenticationResultModel
    {
        public long ContactId { get; set; }
        public AuthenticationResult Result { get; set; }
    }

    public enum AuthenticationResult: byte
    {
        InvalidPassword = 0,
        Accepted,
        MfaVerificationRequired,
        MfaRegistrationRequired
    }
}
