using DataAccess.Models;
using DataAccess.Repositories.Implementation;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public static class DataAccessRegistry
    {
        public static void RegisterServices(IServiceCollection services)
        {
            _ = services.AddScoped<IConvertRepository, ConvertRepository>();
            _ = services.AddScoped<IBestRatesRepository, BestRatesRepository>();
        }
        public static void RegisterDBContext(IServiceCollection services, string connectionString)
        {
            _ = services.AddDbContext<TelegramBotDbContext>(_ => _.UseSqlServer(connectionString));
        }
    }
}