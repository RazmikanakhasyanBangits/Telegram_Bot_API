using Core.Profiles;
using DataAccess;
using ExchangeBot.Abstraction;
using ExchangeBotAdmin.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace ExchangeBotAdmin.Helper
{
    public static class ServiceRegistry
    {
        public static ServiceCollection RegisterServices(this ServiceCollection services)
        {
            _ = services.AddScoped<ICommandHandler, TelegramCommandHandler>();

            _ = services.AddSingleton(LoadConfiguration());
            _ = services.AddAutoMapper(typeof(RatesProfile));

            return services;
        }

        public static ServiceCollection RegisterRepositories(this ServiceCollection services)
        {

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
