using LegionSociety.Contacts.Data.Models;
using LegionSociety.Contacts.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Contact = LegionSociety.Contacts.Data.Models.Contact;


namespace LegionSociety.Contacts.Services.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IPasswordHashService PasswordHashService;
        private readonly IRepository<Contact> ContactRepository;

        public AuthenticationService(IPasswordHashService passwordHashService,
            IRepository<Contact> contactRepository)
        {
            this.PasswordHashService = passwordHashService;
            this.ContactRepository = contactRepository;
        }

        public Contact Validate(string emailAddress, string password)
        {
            var contact = ContactRepository.GetAll().Where(x => x.EmailAddress == emailAddress).SingleOrDefault();
            var pwValid = PasswordHashService.Verify(password, contact?.Password ?? "dummy");

            if (!pwValid || contact == null)
                return null;

            return contact;
        }
    }
}
