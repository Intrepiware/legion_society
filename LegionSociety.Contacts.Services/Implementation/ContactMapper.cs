using System;
using System.Collections.Generic;
using System.Text;
using LegionSociety.Contacts.Data.Models;
using LegionSociety.Contacts.Models;
namespace LegionSociety.Contacts.Services.Implementation
{
    public class ContactMapper : IContactMapper
    {
        public Models.ContactDetailModel MapDetail(Data.Models.Contact contact)
        {
            return new Models.ContactDetailModel
            {
                EmailAddress = contact.EmailAddress,
                FirstName = contact.FirstName,
                Id = contact.Id,
                LastName = contact.LastName,
                Role = (Role)contact.RoleId
            };
        }

        public ContactEditModel MapEdit(Contact contact)
        {
            return new ContactEditModel
            {
                DateOfBirth = contact.DateOfBirth,
                EmailAddress = contact.EmailAddress,
                FirstName = contact.FirstName,
                Id = contact.Id,
                LastName = contact.LastName
            };
        }
    }

    public interface IContactMapper
    {
        Contacts.Models.ContactDetailModel MapDetail(Data.Models.Contact contact);
        ContactEditModel MapEdit(Contact contact);
    }
}
