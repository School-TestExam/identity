using Exam.Services.Identity.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Exam.Services.Identity.IntegrationTests
{
    public class TestBase : IClassFixture<ApiWebApplicationFactory>, IDisposable
    {
        protected IdentityContext _context;
        protected HttpClient _client;
        protected readonly IServiceScope _scope;
        public TestBase(ApiWebApplicationFactory webFactory)
        {
            _client = webFactory.CreateClient();
            var Server = webFactory.Server;
            _scope = Server.Services.CreateScope();

            _context = _scope.ServiceProvider.GetRequiredService<IdentityContext>();
        }
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _scope.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
