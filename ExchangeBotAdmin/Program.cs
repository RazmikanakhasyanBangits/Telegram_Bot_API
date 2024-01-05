using ExchangeBot.Abstraction;
using ExchangeBotAdmin.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ExchangeBotAdmin
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

            ICommandHandler telegram = scope.ServiceProvider.GetRequiredService<ICommandHandler>();

            telegram.Get();
        }
        private static ServiceCollection Load(IConfiguration configuration)
        {
            ServiceCollection serviceCollection = new();

            _ = ServiceRegistry.RegisterServices(serviceCollection);
            return serviceCollection;
        }
        #endregion
    }
}
