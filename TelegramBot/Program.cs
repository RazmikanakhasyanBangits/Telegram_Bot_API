using Core.Services.Interfaces;
using ExchangeBot.Abstraction;
using ExchangeBot.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TelegramBot.Helper;

namespace ExchangeBot
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
            //ILocation location = scope.ServiceProvider.GetRequiredService<ILocation>();
            ICommandHandler telegram = scope.ServiceProvider.GetRequiredService<ICommandHandler>();

            // GetLocationResponseModel result = await location.GetLocationsAsync(nameof(AmeriaBankDataScrapper));
            telegram.StartBot();
        }
        private static ServiceCollection Load(IConfiguration configuration)
        {
            ServiceCollection serviceCollection = new();


            _ = serviceCollection.RegisterDbContext(configuration["ConnectionString"]);
            _ = serviceCollection.RegisterRepositories();
            _ = serviceCollection.RegisterServices();
            return serviceCollection;
        }
        #endregion
    }
}
