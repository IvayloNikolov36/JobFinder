using JobFinder.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data.Seeders
{
    public class GenderSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<Gender>().HasData(
                new Gender { Id = 1, Name = "Not Specified" },
                new Gender { Id = 2, Name = "Male" },
                new Gender { Id = 3, Name = "Female" });
        }
    }
}
