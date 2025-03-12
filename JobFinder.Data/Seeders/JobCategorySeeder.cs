using JobFinder.Data.Models.Nomenclature;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data.Seeders
{
    static class JobCategorySeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<JobCategoryEntity>().HasData(
                new JobCategoryEntity { Name = "Aviation", Id = 1 },
                new JobCategoryEntity { Name = "Healthcare", Id = 2 },
                new JobCategoryEntity { Name = "IT Software Development", Id = 3 },
                new JobCategoryEntity { Name = "IT Hardware Support", Id = 4 },
                new JobCategoryEntity { Name = "Marketing", Id = 5 },
                new JobCategoryEntity { Name = "Cleaning Service", Id = 6 },
                new JobCategoryEntity { Name = "Engeneering", Id = 7 },
                new JobCategoryEntity { Name = "Advertisement", Id = 8 },
                new JobCategoryEntity { Name = "Human Resources", Id = 9 },
                new JobCategoryEntity { Name = "Tourism", Id = 10 },
                new JobCategoryEntity { Name = "Architecture", Id = 11 },
                new JobCategoryEntity { Name = "Production", Id = 12 }
                );
        }
    }
}
