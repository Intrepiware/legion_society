using System;
using System.ComponentModel.DataAnnotations;

namespace LegionSociety.Contacts.Web.Models.Contacts
{
    public class EditModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime? DateOfBirth { get; set; }
    }
}
