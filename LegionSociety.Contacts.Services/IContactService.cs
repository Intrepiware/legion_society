using LegionSociety.Contacts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LegionSociety.Contacts.Services
{
    public interface IContactService
    {
        Task<string> Update(Contact contact);
    }
}
