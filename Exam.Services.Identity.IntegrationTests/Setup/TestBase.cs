using Exam.Services.Identity.Persistence;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Exam.Services.Identity.IntegrationTests.Setup;

[Collection("mysql")]
public class TestBase : IClassFixture<ApiWebApplicationFactory>, IDisposable
{
    protected TestServer Server;
    protected HttpClient Client;
    protected IdentityContext Context;

    private readonly IServiceScope _scope;
        
    public TestBase(ApiWebApplicationFactory factory)
    {
        Server = factory.Server;
        Client = factory.CreateClient();
        _scope = Server.Services.CreateScope();

        Context = _scope.ServiceProvider.GetRequiredService<IdentityContext>();

        try
        {
            Context.Database.EnsureCreated();
        }
        catch 
        {
            // Already Exists
        }
    }
    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        _scope.Dispose();
        GC.SuppressFinalize(this);
    }
}