using LegionSociety.Contacts.Data.Models;
using LegionSociety.Contacts.Services;
using LegionSociety.Contacts.Services.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LegionSociety.Contacts.Web.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IRepository<Contact> contactsRepo;
        private readonly IContactMapper contactMapper;

        public ContactsController(IRepository<Contact> contactsRepo,
            IContactMapper contactMapper)
        {
            this.contactsRepo = contactsRepo;
            this.contactMapper = contactMapper;
        }
        // GET: ContactsController
        public ActionResult Index()
        {
            var models = contactsRepo.GetAll().ToList();
            var contacts = models.Select(contactMapper.Map).ToList();
            return View(contacts);
        }

        // GET: ContactsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ContactsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ContactsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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
