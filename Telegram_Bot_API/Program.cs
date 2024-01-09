using API.Extensions;
using API.GrpcServer;
using Core;
using Core.Services.Bot;
using Core.Services.Bot.Abstraction;
using Core.Services.DataScrapper.Impl;
using Core.Services.Implementations;
using Core.Services.Interfaces;
using DataAccess;
using DataAccess.Repositories.Implementation;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Shared.Infrastructure;
using Shared.Models.Static;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapperConfigurations();
builder.Services.AddControllers();


builder.Services.AddGrpc();

ServiceRegistry.RegisterServices(builder.Services);
RepositoryRegistry.RegisterRepositories(builder.Services);
RepositoryRegistry.RegisterDbContext(builder.Services, builder.Configuration["ConnectionStrings:Default"]);

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
app.MapGrpcService<RateActionsServer>();
_ = app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllers();
});
app.Run();