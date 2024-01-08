using Core.Profiles;
using Core.Services.Implementations;
using Core.Services.Interfaces;
using DataAccess;
using DataAccess.Repositories.Implementation;
using DataAccess.Repositories.Interfaces;
using DataScrapper.Impl;
using ExchangeBot.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure;
using System.IO;
using TelegramBot.Helper;

namespace ExchangeBot.Helper
{
    public static class ServiceRegistry
    {
        public static ServiceCollection RegisterServices(this ServiceCollection services)
        {
            _ = services.AddScoped<IBankService, BankService>();
            _ = services.AddScoped<IBankRepository, BankRepository>();
            _ = services.AddScoped<IBestRateService, BestRateService>();
            _ = services.AddTransient<IUserActivityHistoryService, UserActivityHistoryService>();
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
            _ = services.AddTransient<IBankRepository, BankRepository>();
            _ = services.AddTransient<ILocationRepository, LocationRepository>();
            _ = services.AddTransient<IBestRatesRepository, BestRatesRepository>();
            _ = services.AddTransient<ICurrencyRepository, CurrencyRepository>();
            _ = services.AddTransient<IConvertRepository, ConvertRepository>();
            _ = services.AddTransient<IChatDetailRepository, ChatDetailRepository>();
            _ = services.AddTransient<IUserActivityHistoryRepository, UserActivityHistoryRepository>();
            return services;
        }
        public static ServiceCollection RegisterDbContext(this ServiceCollection services, string connectionString)
        {
            _ = services.AddDbContext<TelegramBotDbContext>(_ => _.UseSqlServer(connectionString),ServiceLifetime.Scoped);
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
