using Exam.Services.Identity.Persistence;
using Exam.Services.Identity.Services;
using Exam.Tools.Tests;
using Microsoft.EntityFrameworkCore;

namespace Exam.Services.Identity.UnitTests
{
    public abstract class TestBase
    {
        //protected readonly IdentityContext _context;

        //protected TestBase()
        //{
        //    var options = new DbContextOptionsBuilder<IdentityContext>()
        //        .UseInMemoryDatabase(databaseName: "InMemoryIdentityDb")
        //        .Options;

        //    _context = new IdentityContext(options);
        //    SeedDatabase(_context);
        //}

        //private void SeedDatabase(IdentityContext context)
        //{
        //    // Add any seed data here if necessary
        //    context.Identities.Add(new Models.Entities.Identity.Identity
        //    {
        //        // Initialize properties
        //    });
        //    context.SaveChanges();
        //}

        //protected IIdentityService CreateIdentityService()
        //{
        //    return new IdentityService(_context);
        //}
    }
}
