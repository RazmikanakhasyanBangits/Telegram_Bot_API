using Core.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TelegramBot.Helper;

namespace TelegramBot
{
    public class Program
    {
        [Obsolete]
        private static void Main(string[] args)
        {
            Init();
        }
        #region Private
        [Obsolete]
        private static void Init()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            ServiceCollection serviceCollection = Load(configuration);
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            using IServiceScope scope = serviceProvider.CreateScope();

            IBankService bankService = scope.ServiceProvider.GetRequiredService<IBankService>();
            CommandSwitcher switcher = scope.ServiceProvider.GetRequiredService<CommandSwitcher>();
            TelegramCommandHandler telegram = new(bankService, configuration);
            telegram.Get();
        }
        [Obsolete]
        private static ServiceCollection Load(IConfiguration configuration)
        {
            ServiceCollection serviceCollection = new();


            _ = ServiceRegistry.RegisterDbContext(serviceCollection, configuration["ConnectionString"]);
            _ = ServiceRegistry.RegisterRepositories(serviceCollection);
            _ = ServiceRegistry.RegisterServices(serviceCollection);
            return serviceCollection;
        }
        #endregion
    }
}
