using Core.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class AutoMapperExtension
    {
        public static void AddAutoMapperConfigurations(this IServiceCollection services)
        {
            _ = services.AddAutoMapper(typeof(RatesProfile));
        }
    }
}
