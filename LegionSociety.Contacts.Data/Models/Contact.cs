using System;
using System.Collections.Generic;
using System.Text;

namespace LegionSociety.Contacts.Data.Models
{
    public class Contact
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }
}
