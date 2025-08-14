using Microsoft.EntityFrameworkCore;

namespace JobFinder.Web.Infrastructure.Extensions;

public static class DbContextOptionsBuilderExtensions
{
    public static void Configure(this DbContextOptionsBuilder builder, IConfiguration configuration)
    {
        builder
            .UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }
}
