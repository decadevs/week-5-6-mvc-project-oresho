using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ResumePortal.Models.Entities;

namespace ResumePortal.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<WorkHistoryEntry> WorkHistories { get; set; }
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {

        }
    }
}
