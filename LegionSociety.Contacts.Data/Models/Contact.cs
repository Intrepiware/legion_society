using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LegionSociety.Contacts.Data.Models
{
    [Table("Contact")]
    public class Contact
    {
        [Key]
        public long Id { get; set; }
        [Required, MaxLength(255)]
        public string FirstName { get; set; }
        [Required, MaxLength(255)]
        public string LastName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public byte RoleId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? InactiveDate { get; set; }
        public ICollection<Invitation> Invitations { get; set;}
    }
}
