using System.Collections.Generic;

namespace LegionSociety.Contacts.Web.Models.TopMenu
{
    public class TopMenuModel
    {
        public List<MenuItemModel> MenuItems { get; set; }
    }

    public class MenuItemModel
    {
        public string DisplayName { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
    }
}
