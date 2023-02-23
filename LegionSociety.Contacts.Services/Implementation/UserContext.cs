using LegionSociety.Contacts.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace LegionSociety.Contacts.Services.Implementation
{
    public class UserContext : IUserContext
    {
        private readonly ClaimsPrincipal User;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            this.User = httpContextAccessor.HttpContext.User;
        }

        public bool CanReadAllContacts()
        {
            return User?.HasClaim($"{LegionSocietyClaimTypes.Contacts}{LegionSocietyClaimTypes.All}{LegionSocietyClaimTypes.Read}", string.Empty) ?? false;
        }

        public bool CanManageContact(long id)
        {
            return CanManageContacts() ||
                (User?.HasClaim($"{LegionSocietyClaimTypes.Contacts}/{id}{LegionSocietyClaimTypes.Manage}", string.Empty) ?? false);
        }

        public bool CanManageContacts()
        {
            return User?.HasClaim($"{LegionSocietyClaimTypes.Contacts}{LegionSocietyClaimTypes.All}{LegionSocietyClaimTypes.Manage}", string.Empty) ?? false;
        }

        public string GetEmailAddress()
        {
            if(User != null)
            {
                return User.FindFirst(ClaimTypes.Email)?.Value;
            }
            return null;
        }

        public long? GetId()
        {
            if(User != null)
            {
                if (long.TryParse(User.FindFirst(LegionSocietyClaimTypes.Id)?.Value, out var id))
                    return id;
            }

            return null;
        }
    }
}
