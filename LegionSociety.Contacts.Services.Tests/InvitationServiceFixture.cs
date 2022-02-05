using FakeItEasy;
using LegionSociety.Contacts.Data.Models;
using LegionSociety.Contacts.Services.Implementation;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LegionSociety.Contacts.Services.Tests
{
    public class InvitationServiceFixture
    {
        [TestFixture]
        public class When_Creating_Invitations
        {
            IRepository<Contact> _contactRepository = null;
            IRepository<Invitation> _invitationRepository = null;
            IUserContext _userContext = null;
            InvitationService _invitationService = null;
            Invitation _addedInvitation = null;

            [SetUp]
            public void Initialize()
            {
                _contactRepository = A.Fake<IRepository<Contact>>();
                _invitationRepository = A.Fake<IRepository<Invitation>>();
                _userContext = A.Fake<IUserContext>();
                _addedInvitation = null;

                var contacts = new List<Contact>()
                {
                    new Contact() { Id = 1, EmailAddress = "user1@email.com" },
                    new Contact() { Id = 2, EmailAddress = "existinguser@email.com"}
                };

                var invitations = new List<Invitation>()
                {
                    new Invitation() { EmailAddress = "existinginvitation@email.com"}
                };

                A.CallTo(() => _userContext.GetId()).Returns(1);
                A.CallTo(() => _contactRepository.GetById(1)).Returns(contacts.First(x => x.Id == 1));
                A.CallTo(() => _contactRepository.GetAll()).Returns(contacts.AsQueryable());
                A.CallTo(() => _invitationRepository.GetAll()).Returns(invitations.AsQueryable());
                A.CallTo(() => _invitationRepository.Add(null))
                    .WithAnyArguments()
                    .ReturnsLazily((Invitation invite) =>
                    {
                        _addedInvitation = invite;
                        return Task.CompletedTask;
                    });

                _invitationService = new InvitationService(_contactRepository, _invitationRepository, _userContext);
            }

            [Test]
            public void Should_Create_Invitation()
            {
                var output = _invitationService.Create("newinvitation@email.com");

                Assert.IsNotNull(output);
                Assert.IsNotNull(_addedInvitation);
                Assert.AreEqual("newinvitation@email.com", _addedInvitation.EmailAddress);
            }

            [Test]
            public void Should_Not_Return_Outstanding_Invitation()
            {
                var output = _invitationService.Create("existinginvitation@email.com");

                Assert.IsNotNull(output);
                Assert.AreEqual("existinginvitation@email.com", output.EmailAddress);
                Assert.IsNull(_addedInvitation);
            }

            [Test]
            public void Should_Not_Create_For_Existing_Contact()
            {
                var output = _invitationService.Create("existinguser@email.com");

                Assert.IsNull(output);
                Assert.IsNull(_addedInvitation);
            }
        }
    }
}
