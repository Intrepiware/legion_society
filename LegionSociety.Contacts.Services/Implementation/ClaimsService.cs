using LegionSociety.Contacts.Data.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace LegionSociety.Contacts.Services.Implementation
{
    public class ClaimsService : IClaimsService
    {
        public List<Claim> Get(Contact contact)
        {
            var output = new List<Claim>();

            if(contact != null)
            {
                output.Add(new Claim(ClaimTypes.Email, contact.EmailAddress));
                output.Add(new Claim(Models.LegionSocietyClaimTypes.Id, contact.Id.ToString()));

                if (contact.RoleId == (byte)Models.Role.Admin)
                {
                    output.Add(new Claim($"{Models.LegionSocietyClaimTypes.Contacts}{Models.LegionSocietyClaimTypes.Manage}", string.Empty));
                }
            }

            return output;
        }

    }
}
