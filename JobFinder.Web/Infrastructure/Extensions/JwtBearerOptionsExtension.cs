using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JobFinder.Web.Infrastructure.Extensions
{
    public static class JwtBearerOptionsExtension
    {
        public static void Configure(
            this JwtBearerOptions options,
            IConfiguration configuration)
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = configuration["JwtAudience"],
                ValidIssuer = configuration["JwtIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSecurityKey"]))
            };
        }
    }
}
