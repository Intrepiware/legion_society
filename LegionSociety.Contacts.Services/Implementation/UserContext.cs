using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace LegionSociety.Contacts.Services.Implementation
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor HttpContextAccessor;
        private readonly ClaimsPrincipal User;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            this.HttpContextAccessor = httpContextAccessor;
            this.User = httpContextAccessor.HttpContext.User;
        }

        public bool CanEditContact(long id)
        {
            return CanManageContacts() || GetId() == id;
        }

        public bool CanManageContacts()
        {
            return User?.HasClaim($"{Models.LegionSocietyClaimTypes.Contacts}{Models.LegionSocietyClaimTypes.Manage}", string.Empty) ?? false;
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
                if (long.TryParse(User.FindFirst(Models.LegionSocietyClaimTypes.Id).Value, out var id))
                    return id;
            }

            return null;
        }
    }
}
