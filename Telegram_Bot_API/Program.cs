using API.Extensions;
using Core;
using DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Shared.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapperConfigurations();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
});

builder.Services.AddSingleton<ISettingsProvider, ApiSettingsProvider>();
ServiceRegistry.RegisterServices(builder.Services);
DataAccessRegistry.RegisterServices(builder.Services);
DataAccessRegistry.RegisterDBContext(builder.Services, builder.Configuration["ConnectionStrings:Default"]);

RepositoryRegistry.RegisterRepositories(builder.Services);

Log.Logger = new LoggerConfiguration()
           .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
           .CreateLogger();

WebApplication app = builder.Build();
if (builder.Environment.IsDevelopment())
{
    _ = app.UseDeveloperExceptionPage();
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
}

_ = app.UseHttpsRedirection();

_ = app.UseRouting();

_ = app.UseAuthorization();

_ = app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllers();
});
app.Run();