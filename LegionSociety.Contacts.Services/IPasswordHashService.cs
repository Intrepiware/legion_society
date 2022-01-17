using System;
using System.Collections.Generic;
using System.Text;

namespace LegionSociety.Contacts.Services
{
    public interface IPasswordHashService
    {
        string Hash(string key);
        bool Verify(string password, string hash);
    }
}
