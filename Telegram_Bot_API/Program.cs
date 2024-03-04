using Api.TelegramBot.Extensions;
using Api.TelegramBot.GrpcServer;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository;
using Serilog;
using Service;
using Service.Model.Models.Rates;
using Service.Model.Models.Static;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapperConfigurations();
builder.Services.AddControllers();


builder.Services.AddGrpc();

ServiceRegistry.RegisterServices(builder.Services);
RepositoryRegistry.RegisterRepositories(builder.Services);
RepositoryRegistry.RegisterDbContext(builder.Services, builder.Configuration["ConnectionStrings:Default"]);


builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<GetAllRatesRequestModelValidator>();

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