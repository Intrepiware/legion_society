using System.ComponentModel.DataAnnotations;

namespace LegionSociety.Contacts.Data.Models
{
    public class Role
    {
        [Key]
        public byte Id { get; set; }
        [MaxLength(20)]
        public string Description { get; set; }
    }
}
