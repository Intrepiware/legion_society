using LegionSociety.Contacts.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegionSociety.Contacts.Services.Implementation
{
    public class ClaimsBasedAuthenticationService : IAuthenticationService
    {
        private readonly IPasswordHashService PasswordHashService;
        private readonly IRepository<Contact> ContactRepository;

        public ClaimsBasedAuthenticationService(IPasswordHashService passwordHashService,
            IRepository<Contact> contactRepository)
        {
            this.PasswordHashService = passwordHashService;
            this.ContactRepository = contactRepository;
        }

        public bool Validate(string emailAddress, string password)
        {
            var contact = ContactRepository.GetAll().Where(x => x.EmailAddress == emailAddress).SingleOrDefault();
            var valid = PasswordHashService.Verify(password, contact?.Password ?? "dummy");

            if (!valid)
                return false;

            // TODO: Add claims, etc.
            return true;
        }
    }
}
