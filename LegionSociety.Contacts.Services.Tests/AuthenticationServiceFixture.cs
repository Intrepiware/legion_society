using FakeItEasy;
using LegionSociety.Contacts.Data.Models;
using LegionSociety.Contacts.Models;
using LegionSociety.Contacts.Services.Implementation;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace LegionSociety.Contacts.Services.Tests
{
    public class AuthenticationServiceFixture
    {
        [TestFixture]
        public class When_Validating_Login
        {
            IRepository<Contact> _contactRepository = null;
            IPasswordHashService _passwordHashService = null;
            AuthenticationService _authenticationService = null;

            [SetUp]
            public void Initialize()
            {
                _contactRepository = A.Fake<IRepository<Contact>>();
                _passwordHashService = A.Fake<IPasswordHashService>();

                var contacts = new List<Contact>()
                {
                    new Contact { Id = 1, EmailAddress = "gooduser@email.com", Password = "password1" },
                    new Contact { Id = 2, EmailAddress = "baduser@email.com", Password="password2"}
                };
                A.CallTo(() => _contactRepository.GetAll()).Returns(contacts.AsQueryable());
                A.CallTo(() => _passwordHashService.Verify(null, null))
                    .WithAnyArguments()
                    .ReturnsLazily((string x, string y) => x == y);

                _authenticationService = new AuthenticationService(_passwordHashService, _contactRepository);
            }

            [Test]
            public void Should_Allow_Valid_Login()
            {
                var result = _authenticationService.Validate("gooduser@email.com", "password1");

                Assert.IsNotNull(result);
                Assert.AreEqual(AuthenticationResult.MfaRegistrationRequired, result.Result);
                Assert.AreEqual(1L, result.ContactId);
            }

            [Test]
            public void Should_Deny_Invalid_Login()
            {
                var result = _authenticationService.Validate("baduser@email.com", "incorrect-password");
                Assert.AreEqual(AuthenticationResult.InvalidPassword, result.Result);
            }

            [Test]
            public void Should_Deny_Invalid_Email()
            {
                var result = _authenticationService.Validate("unknownuser@email.com", "dummy");
                Assert.AreEqual(AuthenticationResult.InvalidPassword, result.Result);
            }
        }
    }
}
