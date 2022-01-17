using LegionSociety.Contacts.Models;
using System.Collections.Generic;

namespace LegionSociety.Contacts.Web.Models.Contacts
{
    public class IndexModel
    {
        public List<Contact> Contacts { get; set; }
        public string EmailAddress { get; set; }
    }
}
