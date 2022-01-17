using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

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
