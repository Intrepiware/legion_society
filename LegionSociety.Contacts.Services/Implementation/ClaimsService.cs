using LegionSociety.Contacts.Data.Models;
using LegionSociety.Contacts.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace LegionSociety.Contacts.Services.Implementation
{
    public class ClaimsService : IClaimsService
    {
        public List<Claim> GetAllClaims(Contact contact)
        {
            var output = new List<Claim>();

            if(contact != null)
            {
                output.Add(new Claim(ClaimTypes.Email, contact.EmailAddress));
                output.Add(new Claim(LegionSocietyClaimTypes.Id, contact.Id.ToString()));
                output.Add(new Claim($"{LegionSocietyClaimTypes.Contacts}{LegionSocietyClaimTypes.All}{LegionSocietyClaimTypes.Read}", string.Empty));
                output.Add(new Claim($"{LegionSocietyClaimTypes.Contacts}/{contact.Id}{LegionSocietyClaimTypes.Manage}", string.Empty));
                

                if (contact.RoleId == (byte)Models.Role.Admin)
                {
                    output.Add(new Claim($"{LegionSocietyClaimTypes.Contacts}{LegionSocietyClaimTypes.All}{LegionSocietyClaimTypes.Manage}", string.Empty));
                }
            }

            return output;
        }

        public List<Claim> GetBasicClaims(Contact contact) => new List<Claim> {
            new Claim(LegionSocietyClaimTypes.Id, contact.Id.ToString())
        };
    }
}
