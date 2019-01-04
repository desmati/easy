using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace Microsoft.Extensions.Configuration
{
    public static class EasySwagger
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, string xmlFileName, string title, string version, string description)
        {
            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new Info { Title = title, Version = version, Description = description });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "AccessToken header using the Bearer scheme. Example: \"Authorization: Bearer {AccessToken}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                // Set the comments path for the Swagger JSON and UI.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, xmlFileName);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app, string description)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", description);
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
            });

            return app;
        }
    }
}
