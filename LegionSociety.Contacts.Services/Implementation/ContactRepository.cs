using LegionSociety.Contacts.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LegionSociety.Contacts.Services.Implementation
{
    public class ContactRepository : Repository<Contact>
    {
        public ContactRepository(DbContext dbContext):base(dbContext)
        {

        }
    }
}
