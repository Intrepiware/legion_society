using LegionSociety.Contacts.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LegionSociety.Contacts.Services.Implementation
{
    public class ContactRepository : Repository<Contact>
    {
        public ContactRepository(DbContext dbContext):base(dbContext)
        {

        }
    }
}
