using Exam.Services.Identity.Persistence;
using Exam.Tools.Tests.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace Exam.Services.Identity.IntegrationTests.Setup;

public class ApiWebApplicationFactory : WebApplicationFactory<Configuration>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.AddTestMySqlContext<IdentityContext>();
        });
    }
}