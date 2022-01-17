using System;
using System.Collections.Generic;
using System.Text;
using LegionSociety.Contacts.Models;
namespace LegionSociety.Contacts.Services.Implementation
{
    public class ContactMapper : IContactMapper
    {
        public Models.Contact Map(Data.Models.Contact contact)
        {
            return new Models.Contact
            {
                EmailAddress = contact.EmailAddress,
                FirstName = contact.FirstName,
                Id = contact.Id,
                LastName = contact.LastName,
                Role = (Role)contact.RoleId
            };
        }
    }

    public interface IContactMapper
    {
        Contacts.Models.Contact Map(Data.Models.Contact contact);
    }
}
