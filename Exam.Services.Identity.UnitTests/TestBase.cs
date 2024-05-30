using Exam.Services.Identity.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Exam.Services.Identity.UnitTests
{
    public class TestBase
    {
        protected readonly IdentityContext Context;

        public TestBase()
        {

            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(databaseName: $"identities-{Guid.NewGuid()}")
                .UseLowerCaseNamingConvention()
                .Options;

            Context = new(options);
        }
    }
}