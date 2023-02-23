using LegionSociety.Contacts.Models;
using LegionSociety.Contacts.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LegionSociety.Contacts.Web.ViewComponents
{
    public class TopMenuViewComponent : ViewComponent
    {
        public IUserContext UserContext { get; }
        public TopMenuViewComponent(IUserContext userContext)
        {
            UserContext = userContext;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new TopMenuModel
            {
                MenuItems = new List<MenuItemModel> { new MenuItemModel { Controller = "Home", Action = "Index", DisplayName = "Home" } }
            };

            if (UserContext.CanReadAllContacts())
            {
                model.MenuItems.Add(new MenuItemModel { Controller = "Contacts", Action = "Index", DisplayName = "Contacts" });
            }
            if(UserContext.GetId() != null)
            {
                model.MenuItems.Add(new MenuItemModel { Controller = "Accounts", Action = "Logout", DisplayName = "Logout" });
            }
            else
            {
                model.MenuItems.Add(new MenuItemModel { Controller = "Accounts", Action = "Login", DisplayName = "Login" });
            }

            await Task.CompletedTask;
            return View("TopMenu", model);
        }
    }
}
