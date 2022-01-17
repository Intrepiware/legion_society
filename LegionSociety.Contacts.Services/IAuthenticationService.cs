using LegionSociety.Contacts.Data.Models;

namespace LegionSociety.Contacts.Services
{
    public interface IAuthenticationService
    {
        Contact Validate(string emailAddress, string password);
    }
}
