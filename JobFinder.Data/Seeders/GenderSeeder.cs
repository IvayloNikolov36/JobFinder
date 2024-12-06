using JobFinder.Data.Models;
using JobFinder.Data.Models.Nomenclature;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data.Seeders
{
    public class GenderSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<GenderEntity>().HasData(
                new GenderEntity { Id = 1, Name = "Not Specified" },
                new GenderEntity { Id = 2, Name = "Male" },
                new GenderEntity { Id = 3, Name = "Female" });
        }
    }
}
