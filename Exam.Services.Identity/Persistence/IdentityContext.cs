using Microsoft.EntityFrameworkCore;

namespace Exam.Services.Identity.Persistence;

public class IdentityContext : DbContext
{
    public DbSet<Models.Entities.Identity.Identity> Identities { get; set; }
    public IdentityContext(DbContextOptions options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}