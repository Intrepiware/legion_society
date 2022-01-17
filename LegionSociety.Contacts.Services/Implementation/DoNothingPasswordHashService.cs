using System;
using System.Collections.Generic;
using System.Text;

namespace LegionSociety.Contacts.Services.Implementation
{
    public class DoNothingPasswordHashService : IPasswordHashService
    {
        public string Hash(string key) => key;

        public bool Verify(string password, string hash) => password == hash;
    }
}
