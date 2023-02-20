using LegionSociety.Contacts.Data.Models;
using LegionSociety.Contacts.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using IAuthenticationService = LegionSociety.Contacts.Services.IAuthenticationService;
namespace LegionSociety.Contacts.Web.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IAuthenticationService AuthenticationService;
        private readonly IClaimsService ClaimsService;
        private readonly IUserContext UserContext;
        private readonly IRepository<Contact> ContactRepository;
        public AccountsController(IAuthenticationService authenticationService,
            IClaimsService claimsService,
            IUserContext userContext,
            IRepository<Contact> contactRepository)
        {
            this.AuthenticationService = authenticationService;
            this.ClaimsService = claimsService;
            UserContext = userContext;
            ContactRepository = contactRepository;
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
        public async Task<IActionResult> Login(string emailAddress, string password, string returnUrl = null)
        {
            var result = AuthenticationService.Validate(emailAddress, password);

            if(result != null)
            {
                var claims = ClaimsService.Get(result);
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principal);
                return LocalRedirect(returnUrl ?? "/Contacts");
            }

            return View();
        }

        public async Task<IActionResult> QrCode()
        {
            if(UserContext.GetId() == null)
            {
                Response.StatusCode = 400;
                return new EmptyResult();
            }

            var qrResponse = await AuthenticationService.CreateTotp(UserContext.GetId().Value);
            if(qrResponse == null)
            {
                Response.StatusCode = 400;
                return new EmptyResult();
            }

            return File(qrResponse.QrImage, "image/png");
        }
    }
}
