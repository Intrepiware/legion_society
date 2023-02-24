using LegionSociety.Contacts.Models;

namespace LegionSociety.Contacts.Web.Models.Contacts
{
    public class DetailModel
    {
        public ContactDetailModel Contact { get; set; }
        public bool CanManageContact { get; set; }
    }
}
