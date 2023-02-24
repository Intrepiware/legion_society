using LegionSociety.Contacts.Data.Models;
using LegionSociety.Contacts.Models;
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
            if (!UserContext.CanReadAllContacts())
                return RedirectToAction("Login", "Accounts");

            var models = ContactsRepo.GetAll().ToList();
            var contacts = models.Select(ContactMapper.MapDetail).ToList();
            var model = new IndexModel { Contacts = contacts, EmailAddress = UserContext.GetEmailAddress() };
            return View(model);
        }

        [Route("Contacts/{id}")]
        public async Task<ActionResult> Details(long id)
        {
            if (!UserContext.CanReadAllContacts())
                return RedirectToAction("Login", "Accounts");

            var contact = await ContactService.Get(id);

            if (contact == null)
                return NotFound();

            var model = new DetailModel
            {
                Contact = contact,
                CanManageContact = UserContext.CanManageContact(id)
            };

            return View("Details", model);
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
            if(UserContext.CanManageContact(id))
            {
                var contact = await ContactService.GetEdit(id);
                if(contact != null)
                {
                    return View(contact);
                }
            }

            return NotFound();
        }

        // POST: ContactsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ContactEditModel model)
        {
            if(ModelState.IsValid)
            {
                ContactService.Update(model);
            }

            return View(model);
        }

    }
}
