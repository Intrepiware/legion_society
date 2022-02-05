using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LegionSociety.Contacts.Data.Models
{
    [Table("Invitation")]
    [Index(nameof(PublicId), IsUnique = true)]
    public class Invitation
    {
        public long Id { get; set; }
        [Required]
        public Guid PublicId { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public Contact InvitingContact { get; set; }
        public DateTime? RedeemDate { get; set; }
        public Contact Contact { get; set; }
    }
}
