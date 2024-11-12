using Contacts.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Contacts.WebApi
{
    public class ApplicationDbContext : DbContext   // memory representation of the database
    {
        public DbSet<Contact> Contacts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }


    }
}
