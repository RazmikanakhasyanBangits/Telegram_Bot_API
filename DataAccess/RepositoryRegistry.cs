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
            _ = services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            _ = services.AddScoped<IBankRepository, BankRepository>();
            _ = services.AddScoped<IRatesRepository, RatesRepository>();
            services.AddScoped<IUserActivityHistoryRepository, UserActivityHistoryRepository>();
            services.AddScoped<IChatDetailRepository, ChatDetailRepository>();
        }
        public static void RegisterDbContext(IServiceCollection services, string connectionString)
        {
            _ = services.AddDbContext<TelegramBotDbContext>(_ => _.UseSqlServer(connectionString));
        }
    }
}
