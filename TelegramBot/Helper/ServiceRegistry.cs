using Core.Profiles;
using Core.Services.Implementations;
using Core.Services.Interfaces;
using DataAccess;
using DataAccess.Repositories.Implementation;
using DataAccess.Repositories.Interfaces;
using DataScrapper.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure;
using System.IO;
using TelegramBot.Abstraction;

namespace TelegramBot.Helper
{
    public static class ServiceRegistry
    {
        public static ServiceCollection RegisterServices(this ServiceCollection services)
        {
            _ = services.AddScoped<IBankService, BankService>();
            _ = services.AddScoped<IBankRepository, BankRepository>();
            _ = services.AddScoped<IBestRateService, BestRateService>();
            _ = services.AddScoped<ISettingsProvider, ApiSettingsProvider>();
            _ = services.AddScoped<ICurrencyService, CurrencyService>();
            _ = services.AddScoped<ICurrencies, Currencies>();
            _ = services.AddScoped<ILocation, Location>();
            _ = services.AddScoped<ILocationService, LocationService>();

            _ = services.AddSingleton(LoadConfiguration());
            _ = services.AddSingleton<ICommandHandler, TelegramCommandHandler>();
            _ = services.AddScoped<AmeriaBankDataScrapper>();
            _ = services.AddScoped<CommandSwitcher>();
            _ = services.AddScoped<TelegramCommandHandler>();
            _ = services.AddAutoMapper(typeof(RatesProfile));

            return services;
        }

        public static ServiceCollection RegisterRepositories(this ServiceCollection services)
        {
            _ = services.AddScoped<IBankRepository, BankRepository>();
            _ = services.AddScoped<ILocationRepository, LocationRepository>();
            _ = services.AddScoped<IBestRatesRepository, BestRatesRepository>();
            _ = services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            _ = services.AddScoped<IConvertRepository, ConvertRepository>();
            //_ = services.AddScoped<IGenericRepository, GenericRepository>();
            return services;
        }
        public static ServiceCollection RegisterDbContext(this ServiceCollection services, string connectionString)
        {
            _ = services.AddDbContext<TelegramBotDbContext>(_ => _.UseSqlServer(connectionString));
            return services;
        }

        private static IConfiguration LoadConfiguration()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return configurationBuilder.Build();
        }
    }
}
