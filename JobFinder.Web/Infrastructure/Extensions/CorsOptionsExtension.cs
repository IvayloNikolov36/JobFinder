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
                        builder.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader().WithMethods("GET", "POST", "DELETE", "PUT");
                    });
        }
    }
}
