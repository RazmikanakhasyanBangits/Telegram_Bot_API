using Core.Services.Implementations;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public static class ServiceRegistry
    {
        public static IServiceCollection RegisterServices(IServiceCollection services)
        {
            _ = services.AddScoped<ICurrencyService, CurrencyService>();
            _ = services.AddScoped<IBankService, BankService>();
            _ = services.AddScoped<ICurrencies, Currencies>();
            _ = services.AddScoped<IBestRateService, BestRateService>();
            services.AddScoped<IUserActivityHistoryService, UserActivityHistoryService>();
            return services;
        }
        public static IServiceCollection AddSwagger(this IServiceCollection services, string title)
        {
            return services.AddSwaggerGen(delegate (SwaggerGenOptions config)
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = title,
                    Version = "v1"
                });
                config.OperationFilter<RemoveVersionFromParameter>(Array.Empty<object>());
                config.DocumentFilter<ReplaceVersionWithExactValueInPath>(Array.Empty<object>());
                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n" + Environment.NewLine + " Enter your token in the text input below.",
                    Name = HeaderNames.Authorization,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
                config.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "Oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    Array.Empty<string>()
                } });
            });
        }
        public static IApplicationBuilder UseCustomSwagger(this WebApplication app, string swaggerPath,
            string apiPath, bool isDev, bool enableDark = false)
        {
            _ = app.UseSwagger(delegate (SwaggerOptions options)
            {
                options.RouteTemplate = swaggerPath + "/swagger/{documentname}/swagger.json";
                if (!isDev)
                {
                    options.PreSerializeFilters.Add(delegate (OpenApiDocument swagger, HttpRequest httpReq)
                    {
                        swagger.Servers = new List<OpenApiServer>
                        {
                            new OpenApiServer
                            {
                                Url = "https://" + httpReq.Host.Value + "/" + apiPath
                            }
                        };
                    });
                }
            });
            _ = app.UseSwaggerUI(delegate (SwaggerUIOptions options)
            {
                if (enableDark)
                {
                    options.InjectStylesheet("https://atom-cdn.azureedge.net/storage/swagger/SwaggerDark.css");
                }

                options.RoutePrefix = swaggerPath + "/swagger";
                options.SwaggerEndpoint("/" + swaggerPath + "/swagger/v1/swagger.json", "API V1");
            });
            return app;
        }
    }

    #region Internal Services
    internal class ReplaceVersionWithExactValueInPath : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            OpenApiPaths paths = swaggerDoc.Paths;
            swaggerDoc.Paths = new OpenApiPaths();
            foreach (KeyValuePair<string, OpenApiPathItem> item in paths)
            {
                string key = item.Key.Replace("v{version}", swaggerDoc.Info.Version);
                OpenApiPathItem value = item.Value;
                swaggerDoc.Paths.Add(key, value);
            }
        }
    }
    internal class RemoveVersionFromParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters.Count > 0)
            {
                OpenApiParameter item = operation.Parameters.SingleOrDefault((OpenApiParameter x) => x.Name == "version");
                _ = operation.Parameters.Remove(item);
            }
        }
    }
    #endregion
}
