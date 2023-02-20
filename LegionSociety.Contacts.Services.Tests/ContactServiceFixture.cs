using FakeItEasy;
using LegionSociety.Contacts.Data;
using LegionSociety.Contacts.Data.Models;
using LegionSociety.Contacts.Services.Implementation;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace LegionSociety.Contacts.Services.Tests
{
    public class ContactServiceFixture
    {
        [TestFixture]
        public class When_Updating_Contact
        {
            IUserContext _userContext = null;
            IRepository<Data.Models.Contact> _contactRepository = null;
            List<Data.Models.Contact> _contacts = null;

            ContactService _contactService = null;
            Data.Models.Contact _updatedContact = null;

            [SetUp]
            public void Initialize()
            {
                _contacts = new List<Data.Models.Contact>
                {
                    new Data.Models.Contact { Id = 1, FirstName = "Alice", LastName = "Tester", EmailAddress = "user1@email.com" },
                    new Data.Models.Contact { Id = 1000, FirstName = "Bob", LastName = "Tester", EmailAddress = "user1000@email.com" }
                };

                _userContext = A.Fake<IUserContext>();
                _contactRepository = A.Fake<IRepository<Data.Models.Contact>>();
                _updatedContact = null;

                A.CallTo(() => _userContext.CanEditContact(1)).Returns(true);
                A.CallTo(() => _userContext.CanEditContact(1000)).Returns(false);
                A.CallTo(() => _contactRepository.GetById(0))
                    .WithAnyArguments()
                    .ReturnsLazily((object[] ids) => _contacts.FirstOrDefault(x => x.Id == (long)ids[0]));
                A.CallTo(() => _contactRepository.GetAll()).ReturnsLazily(x => _contacts.AsQueryable());
                A.CallTo(() => _contactRepository.Update(null))
                    .WithAnyArguments()
                    .Invokes((Data.Models.Contact x) => _updatedContact = x);

                _contactService = new ContactService(_userContext, null, _contactRepository);
            }

            [Test]
            public async Task Should_Update()
            {
                // Arrange
                var contact = new Models.ContactEditModel { Id = 1, FirstName = "Alex", LastName = "Update Test", EmailAddress="user1@email.com" };

                // Act
                var result = await _contactService.Update(contact);

                // Assert
                Assert.AreEqual(string.Empty, result);
                Assert.IsNotNull(_updatedContact);
                Assert.AreEqual("Alex", _updatedContact.FirstName);
                Assert.AreEqual("Update Test", _updatedContact.LastName);

            }

            [Test]
            public void Should_Throw_If_Unprivileged()
            {
                // Arrange
                var contact = new Models.ContactEditModel { Id = 1000, FirstName = "Brad", LastName = "Update Test", EmailAddress = "user1000@email.com" };

                // Act / Assert
                Assert.ThrowsAsync<SecurityException>(async () => await _contactService.Update(contact));

            }

            [Test]
            public async Task Should_Error_If_Duplicate_Email()
            {

                // Arrange
                var contact = new Models.ContactEditModel { Id = 1, FirstName = "Alex", LastName = "Update Test", EmailAddress = "user1000@email.com" };

                // Act
                var result = await _contactService.Update(contact);

                // Assert
                Assert.AreNotEqual(string.Empty, result);
                StringAssert.Contains("email", result.ToLower()); // The warning should contain something about the email address
                Assert.IsNull(_updatedContact);
            }
        }

        [TestFixture]
        public class When_Running_Integration_Test
        {
            ContactService contactService = null;
            IUserContext _userContext = null;
            IRepository<Contact> _contactRepository = null;

            [SetUp]
            public void Initialize()
            {
                var optionsBuilder = new DbContextOptionsBuilder<ContactContext>();
                optionsBuilder.UseSqlServer("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=LegionSocietyContactsTest;Data Source=(local)");
                var contactContext = new ContactContext(optionsBuilder.Options);

                _contactRepository = new Repository<Contact>(contactContext);

                _userContext = A.Fake<IUserContext>();
                contactService = new ContactService(_userContext, null, _contactRepository);
            }

            [Test]
            public async Task Should_Update()
            {
                var contact = new Contact
                {
                    FirstName = "Dave",
                    LastName = "Tester",
                    CreateDate = System.DateTime.UtcNow,
                    DateOfBirth = System.DateTime.Parse("1/1/1980"),
                    EmailAddress = "dave@example.com",
                    Password = "new",
                    RoleId = 1
                };

                await _contactRepository.Add(contact);

                contact.FirstName = "john";
                contact.EmailAddress = "john@example.com";
                contact.TotpKey = "TESTKEY";
                _contactRepository.Update(contact);
            }
        }
    }
}
