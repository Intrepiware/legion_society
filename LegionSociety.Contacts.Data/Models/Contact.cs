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
        
        [Required, MaxLength(50)]
        public string EmailAddress { get; set; }
        
        [Required, MaxLength(255)]
        public string Password { get; set; }
        
        [Required]
        public byte RoleId { get; set; }
        
        [Column(TypeName ="datetime")]
        public DateTime? DateOfBirth { get; set; }
        
        [Column(TypeName ="datetime")]
        public DateTime CreateDate { get; set; }
        
        [Column(TypeName ="datetime")]
        public DateTime? InactiveDate { get; set; }
        
        public ICollection<Invitation> Invitations { get; set;}
        
        [MaxLength(50)]
        public string TotpKey { get; set; }
        
        [Column(TypeName ="datetime")]
        public DateTime? TotpConfirmDate { get; set; }
    }
}
