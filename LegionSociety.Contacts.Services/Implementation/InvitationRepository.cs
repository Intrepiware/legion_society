using LegionSociety.Contacts.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LegionSociety.Contacts.Services.Implementation
{
    public class InvitationRepository : Repository<Invitation>
    {
        public InvitationRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
