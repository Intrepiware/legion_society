using System;
using System.Collections.Generic;

namespace LegionSociety.Contacts.Models
{
    public class ContactDetailModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Bio { get; set; }
        public string DietaryRestrictions { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime MembershipStartDate { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public Dictionary<long, string> FamilyMembers { get; set; }
    }
}
