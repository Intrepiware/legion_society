using System;
using System.ComponentModel.DataAnnotations;

namespace LegionSociety.Contacts.Models
{
    public class ContactEditModel
    {
        public long Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public DateTime? DateOfBirth { get; set; }
    }
}
