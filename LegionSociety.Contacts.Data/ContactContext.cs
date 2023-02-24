using LegionSociety.Contacts.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LegionSociety.Contacts.Data
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .HasMany(x => x.Invitations)
                .WithOne(x => x.InvitingContact);

            modelBuilder.Entity<Contact>()
                .HasMany(x => x.ContactFamilyMembers)
                .WithOne(x => x.Contact);
        }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
    }
}
