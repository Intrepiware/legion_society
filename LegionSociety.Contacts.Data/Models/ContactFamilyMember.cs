using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LegionSociety.Contacts.Data.Models
{
    [Table("ContactFamilyMember")]
    public class ContactFamilyMember
    {
        [Key]
        public long Id { get; set; }

        public Contact Contact { get; set; }

        [Required, MaxLength(255)]
        public string FirstName { get; set; }

        [Required, MaxLength(255)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string PreferredName { get; set; }

        public string Relationship { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime DateOfBirth { get; set; }

        [MaxLength(2)]
        public string Gender { get; set; }

    }
}
