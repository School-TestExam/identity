using Microsoft.EntityFrameworkCore;

namespace Exam.Services.Identity.Persistence
{
    public class IdentityContext : DbContext
    {
        public IdentityContext(DbContextOptions options) : base(options) { }

        public DbSet<Models.Entities.Identity.Identity> Identities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}