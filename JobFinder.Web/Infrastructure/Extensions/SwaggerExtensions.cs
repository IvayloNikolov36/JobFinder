using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace JobFinder.Web.Infrastructure.Extensions;

public static class SwaggerExtensions
{
    public static void Configure(this SwaggerGenOptions options)
    {
        options.SwaggerDoc(
            name: "v1",
            info: new OpenApiInfo()
            {
                Title = "JobFinder API",
                Version = "v1"
            });
    }

    public static void Configure(this SwaggerUIOptions uiOptions)
    {
        uiOptions
            .SwaggerEndpoint("/swagger/v1/swagger.json", "JobFinder API");
    }
}
