using LegionSociety.Contacts.Data.Models;
using LegionSociety.Contacts.Services;
using LegionSociety.Contacts.Services.Implementation;
using LegionSociety.Contacts.Web.Models.Contacts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LegionSociety.Contacts.Web.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IRepository<Contact> contactsRepo;
        private readonly IContactMapper contactMapper;
        private readonly IUserContext UserContext;

        public ContactsController(IRepository<Contact> contactsRepo,
            IContactMapper contactMapper,
            IUserContext userContext)
        {
            this.contactsRepo = contactsRepo;
            this.contactMapper = contactMapper;
            this.UserContext = userContext;
        }
        // GET: ContactsController
        [Authorize]
        public ActionResult Index()
        {
            var models = contactsRepo.GetAll().ToList();
            var contacts = models.Select(contactMapper.Map).ToList();
            var model = new IndexModel { Contacts = contacts, EmailAddress = UserContext.GetEmailAddress() };
            return View(model);
        }

        // GET: ContactsController/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ContactsController/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ContactsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ContactsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ContactsController/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ContactsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
