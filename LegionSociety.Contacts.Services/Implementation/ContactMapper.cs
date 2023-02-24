using LegionSociety.Contacts.Data.Models;
using LegionSociety.Contacts.Models;
using System;
using System.Linq;

namespace LegionSociety.Contacts.Services.Implementation
{
    public class ContactMapper : IContactMapper
    {
        public ContactDetailModel MapDetail(Contact contact)
        {
            return new ContactDetailModel
            {
                EmailAddress = contact.EmailAddress,
                FirstName = contact.FirstName,
                Id = contact.Id,
                LastName = contact.LastName,
                Role = (Models.Role)contact.RoleId,
                Address1 = contact.Address1,
                Address2 = contact.Address2,
                Bio = contact.Bio,
                City = contact.City,
                DietaryRestrictions = contact.DietaryRestrictions,
                Gender = contact.Gender,
                PhoneNumber = contact.PhoneNumberMain,
                PostalCode = contact.PostalCode,
                State = contact.State,
                MembershipStartDate = contact.MembershipStartDate,
                FamilyMembers = contact.ContactFamilyMembers.ToDictionary(x => x.Id, x => GetFamilyMemberDisplayName(contact.LastName, x)),
                DateOfBirth = contact.DateOfBirth
            };
        }

        private string GetFamilyMemberDisplayName(string contactLastName, ContactFamilyMember familyMember)
        {
            var lastName = string.Equals(contactLastName, familyMember.LastName, StringComparison.OrdinalIgnoreCase) ?
                string.Empty : $" {familyMember.LastName}";
            var now = DateTime.UtcNow.Date;

            var age = now.Year - familyMember.DateOfBirth.Year;
            if (now.Month < familyMember.DateOfBirth.Month || now.Month == familyMember.DateOfBirth.Month && now.Day < familyMember.DateOfBirth.Day)
                age -= 1;

            return $"{familyMember.Relationship}: {familyMember.FirstName}{lastName} ({age})";
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
        ContactDetailModel MapDetail(Data.Models.Contact contact);
        ContactEditModel MapEdit(Contact contact);
    }
}
