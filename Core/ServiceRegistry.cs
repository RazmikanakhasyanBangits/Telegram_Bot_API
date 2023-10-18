using Core.Services;
using Core.Services.Implementations;
using Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public class ServiceRegistry
    {
        public static void RegisterServices(IServiceCollection services)
        {
            _ = services.AddScoped<ICurrencyService, CurrencyService>();
            _ = services.AddScoped<IBankService, BankService>();
            _ = services.AddScoped<ICurrencies, Currencies>();
            _ = services.AddScoped<IBestRateService, BestRateService>();

        }

    }
}
