using System;
using System.Collections.Generic;
using System.Text;

namespace LegionSociety.Contacts.Services
{
    public interface IAuthenticationService
    {
        bool Validate(string emailAddress, string password);
    }
}
