namespace JobFinder.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Cors.Infrastructure;
    using static JobFinder.Web.Infrastructure.WebConstants;

    public static class CorsOptionsExtension
    {
        public static void Configure(this CorsOptions options)
        {
            options.AddPolicy(CorsPolicyName,
                builder =>
                {
                    builder.WithOrigins("http://localhost:4100")
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true)
                    .WithMethods(HttpGet, HttpPost, HttpPut, HttpPatch, HttpDelete);
                });
        }
    }
}
