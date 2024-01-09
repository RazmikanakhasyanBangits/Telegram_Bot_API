using DataAccess.Repositories.Implementation;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public class RepositoryRegistry
    {
        public static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<IBankRepository, BankRepository>();
            services.AddScoped<IRatesRepository, RatesRepository>();
            services.AddScoped<IConvertRepository, ConvertRepository>();
            services.AddScoped<IBestRatesRepository, BestRatesRepository>();
            services.AddScoped<IUserActivityHistoryRepository, UserActivityHistoryRepository>();
            services.AddScoped<IChatDetailRepository, ChatDetailRepository>();
            services.AddTransient<ILocationRepository, LocationRepository>();
        }
        public static void RegisterDbContext(IServiceCollection services, string connectionString)
        {
            _ = services.AddDbContext<TelegramBotDbContext>(_ => _.UseSqlServer(connectionString));
        }
    }
}
