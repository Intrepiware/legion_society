using LegionSociety.Contacts.Data.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace LegionSociety.Contacts.Services
{
    public interface IClaimsService
    {
        List<Claim> Get(Contact contact);
    }
}
