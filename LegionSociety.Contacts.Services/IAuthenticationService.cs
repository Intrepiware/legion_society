using LegionSociety.Contacts.Data.Models;
using LegionSociety.Contacts.Models;
using System.Threading.Tasks;

namespace LegionSociety.Contacts.Services
{
    public interface IAuthenticationService
    {
        Contact Validate(string emailAddress, string password);

        Task<QrResponseModel> CreateTotp(long contactId);
    }
}
