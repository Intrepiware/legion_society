using LegionSociety.Contacts.Data.Models;
using LegionSociety.Contacts.Models;
using System.Threading.Tasks;

namespace LegionSociety.Contacts.Services
{
    public interface IAuthenticationService
    {
        Task<InitializeMfaModel> InitializeMfa(long contactId);
        AuthenticationResultModel Validate(string emailAddress, string password);
        Task<bool> VerifyMfa(long contactId, string code);
    }
}
