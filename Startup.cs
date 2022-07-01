using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using PublicAPIs;

[assembly: FunctionsStartup(typeof(Startup))]
namespace PublicAPIs
{

    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            _ = builder.Services.AddHttpClient();
            _ = builder.Services.AddTransient<IShared, Shared>();
        }
    }
}
