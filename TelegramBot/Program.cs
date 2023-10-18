using Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.IO;
using TelegramBot.Helper;

namespace TelegramBot
{
    public class Program
    {
        [Obsolete]
        private static void Main(string[] args)
        {
            Init(Load());
        }
        #region Private
        [Obsolete]
        private static ServiceCollection Load()
        {
            ServiceCollection serviceCollection = new();
            Settings settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText("settings.json"));

            _ = ServiceRegistry.RegisterDbContext(serviceCollection, settings.ConnectionString);
            _ = ServiceRegistry.RegisterRepositories(serviceCollection);
            _ = ServiceRegistry.RegisterServices(serviceCollection);
            return serviceCollection;
        }

        [Obsolete]
        private static void Init(ServiceCollection serviceCollection)
        {
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            using IServiceScope scope = serviceProvider.CreateScope();

            IBankService bankService = scope.ServiceProvider.GetRequiredService<IBankService>();
            CommandSwitcher switcher = scope.ServiceProvider.GetRequiredService<CommandSwitcher>();
            TelegramCommandHandler telegram = new(bankService);
            telegram.Get();
        }
        #endregion
    }
}
