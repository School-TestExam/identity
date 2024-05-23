using Exam.Abstractions;
using Exam.Core.Persistence.Extensions;
using Exam.Services.Identity.Persistence;
using Exam.Services.Identity.Services;

namespace Exam.Services.Identity;

public class Configuration : ServiceConfiguration
{
    public override void Configure(IApplicationBuilder builder)
    {

    }

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddMySqlContext<IdentityContext>("identity", Configuration);

        services.AddTransient<IIdentityService, IdentityService>();
    }
}