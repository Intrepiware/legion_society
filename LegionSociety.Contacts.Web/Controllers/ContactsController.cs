using LegionSociety.Contacts.Data.Models;
using LegionSociety.Contacts.Services;
using LegionSociety.Contacts.Services.Implementation;
using LegionSociety.Contacts.Web.Models.Contacts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace LegionSociety.Contacts.Web.Controllers
{
    [Authorize]
    public class ContactsController : Controller
    {
        private readonly IRepository<Contact> ContactsRepo;
        private readonly IContactMapper ContactMapper;
        private readonly IContactService ContactService;
        private readonly IUserContext UserContext;

        public ContactsController(IRepository<Contact> contactsRepo,
            IContactMapper contactMapper,
            IContactService contactService,
            IUserContext userContext)
        {
            this.ContactsRepo = contactsRepo;
            this.ContactMapper = contactMapper;
            this.ContactService = contactService;
            this.UserContext = userContext;
        }
        // GET: ContactsController
        public ActionResult Index()
        {
            var models = ContactsRepo.GetAll().ToList();
            var contacts = models.Select(ContactMapper.Map).ToList();
            var model = new IndexModel { Contacts = contacts, EmailAddress = UserContext.GetEmailAddress() };
            return View(model);
        }

        [Route("Contacts/{id}")]
        // GET: ContactsController/5
        public ActionResult Index(long id)
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

        [Route("Contacts/{id}/Edit")]
        // GET: ContactsController/5/Edit
        public async Task<ActionResult> Edit(long id)
        {
            if(UserContext.CanEditContact(id))
            {
                var contact = await ContactsRepo.GetById(id);
                if(contact != null)
                {
                    var model = new EditModel
                    {
                        DateOfBirth = contact.DateOfBirth,
                        Email = contact.EmailAddress,
                        FirstName = contact.FirstName,
                        LastName = contact.LastName
                    };
                    return View(model);
                }
            }

            return NotFound();
        }

        // POST: ContactsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditModel model)
        {
            if(ModelState.IsValid)
            {
                var contact = new Contacts.Models.Contact
                {
                    EmailAddress = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Id = id
                };
                ContactService.Update(contact);
            }

            return View(model);
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
