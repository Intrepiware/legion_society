using LegionSociety.Contacts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LegionSociety.Contacts.Services
{
    public interface IMfaService
    {
        string GenerateKey();
        byte[] GenerateQr(string key, string emailAddress);
        bool Verify(string key, string code);
    }
}
