using JobFinder.Data.Models.Nomenclature;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data.Seeders
{
    class DrivingCategoryTypeSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<DrivingCategoryTypeEntity>().HasData(
                new DrivingCategoryTypeEntity { Id = 1, Category = "AM" },
                new DrivingCategoryTypeEntity { Id = 2, Category = "A1" },
                new DrivingCategoryTypeEntity { Id = 3, Category = "A2" },
                new DrivingCategoryTypeEntity { Id = 4, Category = "B" },
                new DrivingCategoryTypeEntity { Id = 5, Category = "B1" },
                new DrivingCategoryTypeEntity { Id = 6, Category = "C" },
                new DrivingCategoryTypeEntity { Id = 7, Category = "C+E" },
                new DrivingCategoryTypeEntity { Id = 8, Category = "D" });
        }
    }
}
