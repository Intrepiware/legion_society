using LegionSociety.Contacts.Data.Models;
using System;
using System.Linq;

namespace LegionSociety.Contacts.Services.Implementation
{
    public class InvitationService : IInvitationService
    {
        private readonly IRepository<Contact> ContactRepository;
        private readonly IRepository<Invitation> InvitationRepository;
        private readonly IUserContext UserContext;

        public InvitationService(IRepository<Contact> contactRepository,
            IRepository<Invitation> invitationRepository,
            IUserContext userContext)
        {
            this.ContactRepository = contactRepository;
            this.InvitationRepository = invitationRepository;
            this.UserContext = userContext;
        }
        public Invitation Create(string emailAddress)
        {
            if (ContactRepository.GetAll().Any(x => x.EmailAddress == emailAddress))
                return null;

            var output = InvitationRepository.GetAll().SingleOrDefault(x => x.EmailAddress == emailAddress && !x.RedeemDate.HasValue);

            if (output != null)
                return output;

            output = new Invitation
            {
                EmailAddress = emailAddress,
                CreateDate = DateTime.UtcNow,
                InvitingContact = ContactRepository.GetAll().FirstOrDefault(x => x.Id == UserContext.GetId()),
                PublicId = Guid.NewGuid()
            };

            InvitationRepository.Add(output);
            return output;

        }
    }
}
