using ContactManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Contact> Contacts => Set<Contact>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contact>().HasIndex(contact => contact.ContactNumber).IsUnique();

            modelBuilder.Entity<Contact>().HasIndex(contact => contact.Email).IsUnique();

            modelBuilder.Entity<Contact>().HasQueryFilter(contact => !contact.IsDeleted);
        }
    }
}
