using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;

namespace Api.Extensions
{
    public static class AppBuilderExtension
    {
        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, string version, string apiName, string virtualPath)
        {
            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    var url = $"/swagger/{version}/swagger.json";
                    if (!string.IsNullOrWhiteSpace(virtualPath))
                    {
                        url = $"/{virtualPath}/{url}";
                    }

                    c.SwaggerEndpoint(url, $"{apiName} API {version}");

                    c.DocExpansion(DocExpansion.None);
                });

            return app;
        }

    }
}
