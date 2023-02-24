using LegionSociety.Contacts.Data.Models;
using LegionSociety.Contacts.Models;
namespace LegionSociety.Contacts.Services.Implementation
{
    public class ContactMapper : IContactMapper
    {
        public Models.ContactDetailModel MapDetail(Contact contact)
        {
            return new Models.ContactDetailModel
            {
                EmailAddress = contact.EmailAddress,
                FirstName = contact.FirstName,
                Id = contact.Id,
                LastName = contact.LastName,
                Role = (Role)contact.RoleId,
                Address1 = contact.Address1,
                Address2 = contact.Address2,
                Bio = contact.Bio,
                City = contact.City,
                DietaryRestrictions = contact.DietaryRestrictions,
                Gender = contact.Gender,
                PhoneNumber = contact.PhoneNumberMain,
                PostalCode = contact.PostalCode,
                State = contact.State,
                MembershipStartDate = contact.MembershipStartDate
            };
        }

        public ContactEditModel MapEdit(Contact contact)
        {
            return new ContactEditModel
            {
                DateOfBirth = contact.DateOfBirth,
                EmailAddress = contact.EmailAddress,
                FirstName = contact.FirstName,
                Id = contact.Id,
                LastName = contact.LastName
            };
        }
    }

    public interface IContactMapper
    {
        Contacts.Models.ContactDetailModel MapDetail(Data.Models.Contact contact);
        ContactEditModel MapEdit(Contact contact);
    }
}
