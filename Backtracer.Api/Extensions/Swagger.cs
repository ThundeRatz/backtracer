using System.Reflection;
using Backtracer.Api.Attributes;
using Microsoft.OpenApi.Models;

namespace Backtracer.Api.Extensions;

internal static class SwaggerExtension {
    public static void ConfigureSwagger(this IServiceCollection services) {
        services.AddVersionedApiExplorer(options => {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options => {
            options.SwaggerDoc("v1", new OpenApiInfo {
                Version = "v1",
                Title = "Backtracer",
                Description = "Tracer's Web API",
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

            options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme {
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
                Name = ApiKeyAttribute.APIKEYHEADER,
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "ApiKey" }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }

    public static void ConfigureSwagger(this IApplicationBuilder app) {
        app.UseSwagger(options => {
            options.RouteTemplate = "docs/{documentName}/swagger.json";
        });

        app.UseSwaggerUI(options => {
            options.RoutePrefix = "docs";
            options.SwaggerEndpoint("/docs/v1/swagger.json", "Backtracer v1");
            options.InjectStylesheet("/swagger-ui/theme-feeling-blue.css");
            options.InjectStylesheet("/swagger-ui/custom.css");
        });
    }
}
