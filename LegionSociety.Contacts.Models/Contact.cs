using System;
using System.Collections.Generic;
using System.Text;

namespace LegionSociety.Contacts.Models
{
    public class Contact
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
