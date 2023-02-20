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
        private readonly IContactMapper ContactMapper;
        private readonly IRepository<Data.Models.Contact> ContactsRepository;


        public ContactService(IUserContext userContext,
            IContactMapper contactMapper,
            IRepository<Data.Models.Contact> contactsRepository)
        {
            this.UserContext = userContext;
            this.ContactMapper = contactMapper;
            this.ContactsRepository = contactsRepository;
        }

        public async Task<ContactDetailModel> Get(long id)
        {
            var contact = await ContactsRepository.GetById(id);

            if (contact == null)
                return null;

            return ContactMapper.MapDetail(contact);
        }

        public async Task<ContactEditModel> GetEdit(long id)
        {
            var contact = await ContactsRepository.GetById(id);
            if (contact == null)
                return null;
            return ContactMapper.MapEdit(contact);
        }

        public async Task<string> Update(ContactEditModel contact)
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
