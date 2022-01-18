using LegionSociety.Contacts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace LegionSociety.Contacts.Services.Implementation
{
    public class ContactService : IContactService
    {
        private readonly IUserContext UserContext;
        private readonly IRepository<Data.Models.Contact> ContactsRepository;

        public ContactService(IUserContext userContext,
            IRepository<Data.Models.Contact> contactsRepository)
        {
            this.UserContext = userContext;
            this.ContactsRepository = contactsRepository;
        }
        public async Task<string> Update(Contact contact)
        {
            if (UserContext.CanEditContact(contact.Id))
            {
                var record = await ContactsRepository.GetById(contact.Id);

                if (ContactsRepository.GetAll().Any(x => x.Id != contact.Id && x.EmailAddress == contact.EmailAddress))
                    return "Email address already in use";

                record.FirstName = contact.FirstName;
                record.LastName = contact.LastName;
                record.EmailAddress = contact.EmailAddress;
                ContactsRepository.Update(record);
                return string.Empty;
            }
            else
                throw new SecurityException();

        }
    }
}
