using LegionSociety.Contacts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LegionSociety.Contacts.Services
{
    public interface IMfaService
    {
        Task<QrResponseModel> Initialize(long contactId);
        Task<bool> Verify(long contactId, string totp);
    }
}
