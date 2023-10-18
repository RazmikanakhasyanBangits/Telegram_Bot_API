using Core.Profiles;
using Core.Services;
using Core.Services.Implementations;
using Core.Services.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories.Implementation;
using DataAccess.Repositories.Interfaces;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure;
using TelegramBot.Abstraction;
using TelegramBot.Impl;

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
            _ = services.AddSingleton<ICommandHendler, CommandHandler>();
            _ = services.AddScoped<CommandSwitcher>();
            _ = services.AddAutoMapper(typeof(RatesProfile));
            return services;
        }

        public static ServiceCollection RegisterRepositories(this ServiceCollection services)
        {
            _ = services.AddScoped<IBankRepository, BankRepository>();
            _ = services.AddScoped<IBestRatesRepository, BestRatesRepository>();
            _ = services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            _ = services.AddScoped<IConvertRepository, ConvertRepository>();
            return services;
        }
        public static ServiceCollection RegisterDbContext(this ServiceCollection services, string connectionString)
        {
            _ = services.AddDbContext<TelegramBotDbContext>(_ => _.UseSqlServer(connectionString));
            return services;
        }
    }
}
