using System;
using System.Collections.Generic;
using System.Text;

namespace LegionSociety.Contacts.Services
{
    public interface IUserContext
    {
        public long? GetId();
        public string GetEmailAddress();
        public bool CanManageContacts();
        public bool CanManageContact(long id);
        bool CanReadAllContacts();
    }
}
