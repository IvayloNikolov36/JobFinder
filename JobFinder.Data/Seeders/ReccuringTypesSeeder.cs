using JobFinder.Data.Models.Nomenclature;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data.Seeders
{
    static class ReccuringTypesSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<ReccuringTypeEntity>().HasData(
                new ReccuringTypeEntity { Id = 1, Name = "Daily" },
                new ReccuringTypeEntity { Id = 2, Name = "Weekly" },
                new ReccuringTypeEntity { Id = 3, Name = "Monthly" }
            );
        }
    }
}
