using LegionSociety.Contacts.Data.Models;
using LegionSociety.Contacts.Models;
using System.Threading.Tasks;

namespace LegionSociety.Contacts.Services
{
    public interface IAuthenticationService
    {
        AuthenticationResultModel Validate(string emailAddress, string password);
    }
}
