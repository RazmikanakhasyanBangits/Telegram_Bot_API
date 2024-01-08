using API.Extensions;
using API.GrpcServer;
using Core;
using Core.Services.Implementations;
using Core.Services.Interfaces;
using DataAccess;
using DataAccess.Repositories.Implementation;
using DataAccess.Repositories.Interfaces;
using ExchangeBot;
using ExchangeBot.Abstraction;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Shared.Infrastructure;
using Shared.Models.Static;
using Telegram.Bots.Types;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapperConfigurations();
builder.Services.AddControllers();


builder.Services.AddSingleton<ISettingsProvider, ApiSettingsProvider>();
ServiceRegistry.RegisterServices(builder.Services);
builder.Services.AddSwagger(SettingsConstants.SwaggerTitle);
builder.Services.AddScoped<ICommandHandler, TelegramCommandHandler>();
builder.Services.AddScoped<ILocation, DataScrapper.Impl.Location>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
DataAccessRegistry.RegisterServices(builder.Services);
DataAccessRegistry.RegisterDBContext(builder.Services, builder.Configuration["ConnectionStrings:Default"]);
builder.Services.AddGrpc();
RepositoryRegistry.RegisterRepositories(builder.Services);

Log.Logger = new LoggerConfiguration()
           .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
           .CreateLogger();

WebApplication app = builder.Build();
if (builder.Environment.IsDevelopment())
{
    _ = app.UseCustomSwagger(SettingsConstants.SwaggerPrefixPath, SettingsConstants.ApiPath,
        builder.Environment.IsDevelopment(), enableDark: true);
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapGrpcService<UserActionsServer>();
_ = app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllers();
});
app.Run();