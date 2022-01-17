using System;
using System.Collections.Generic;
using System.Text;

namespace LegionSociety.Contacts.Services.Implementation
{
    public class PasswordHashService : IPasswordHashService
    {
        const int Cost = 11;
        public string Hash(string key) => BCrypt.Net.BCrypt.HashPassword(key, workFactor: Cost);

        public bool Verify(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
