using Service.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace Api.TelegramBot.Extensions;

public static class AutoMapperExtension
{
    public static void AddAutoMapperConfigurations(this IServiceCollection services)
    {
        _ = services.AddAutoMapper(typeof(RatesProfile));
    }
}
