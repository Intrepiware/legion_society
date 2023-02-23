﻿using LegionSociety.Contacts.Data.Models;
using LegionSociety.Contacts.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public IMfaService MfaService;
        public AccountsController(IAuthenticationService authenticationService,
            IMfaService mfaService,
            IClaimsService claimsService,
            IUserContext userContext,
            IRepository<Contact> contactRepository)
        {
            this.AuthenticationService = authenticationService;
            MfaService = mfaService;
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

            if(result.ContactId > 0)
            {
                var contact = await ContactRepository.GetById(result.ContactId);
                var claims = ClaimsService.GetBasicClaims(contact);
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principal);

                if (result.Result == Contacts.Models.AuthenticationResult.MfaRegistrationRequired)
                    return RedirectToAction("MfaRegister");
                else
                    return RedirectToAction("MfaVerify");
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> MfaRegister()
        {
            if(UserContext.GetId() == null)
            {
                Response.StatusCode = 400;
                return new EmptyResult();
            }

            var qrResponse = await MfaService.Initialize(UserContext.GetId().Value);
            if(qrResponse == null)
            {
                Response.StatusCode = 400;
                return new EmptyResult();
            }

            return View(qrResponse);
        }

        [HttpPost]
        public async Task<IActionResult> MfaRegister(string mfa)
        {
            return await VerifyMfa(mfa);
        }

        public IActionResult MfaVerify()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MfaVerify(string mfa)
        {
            return await VerifyMfa(mfa);
        }

        private async Task<IActionResult> VerifyMfa(string mfa)
        {
            if (UserContext.GetId() == null)
            {
                Response.StatusCode = 400;
                return new EmptyResult();
            }

            if (await MfaService.Verify(UserContext.GetId().Value, mfa))
            {
                var contact = await ContactRepository.GetById(UserContext.GetId().Value);
                var claims = ClaimsService.GetAllClaims(contact);
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Contacts");
            }
            return View();
        }
    }
}
