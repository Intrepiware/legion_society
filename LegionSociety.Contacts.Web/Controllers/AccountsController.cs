using LegionSociety.Contacts.Services;
using Microsoft.AspNetCore.Mvc;

namespace LegionSociety.Contacts.Web.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IAuthenticationService AuthenticationService;

        public AccountsController(IAuthenticationService authenticationService)
        {
            this.AuthenticationService = authenticationService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string emailAddress, string password)
        {
            var result = AuthenticationService.Validate(emailAddress, password);

            return Content(result ? "Validated": "Try again");
        }
    }
}
