using JobFinder.Data.Models.Nomenclature;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data.Seeders
{
    class DrivingCategorySeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<DrivingCategoryEntity>().HasData(
                new DrivingCategoryEntity { Id = 1, Name = "AM" },
                new DrivingCategoryEntity { Id = 2, Name = "A1" },
                new DrivingCategoryEntity { Id = 3, Name = "A2" },
                new DrivingCategoryEntity { Id = 4, Name = "B" },
                new DrivingCategoryEntity { Id = 5, Name = "B1" },
                new DrivingCategoryEntity { Id = 6, Name = "C" },
                new DrivingCategoryEntity { Id = 7, Name = "C+E" },
                new DrivingCategoryEntity { Id = 8, Name = "D" });
        }
    }
}
