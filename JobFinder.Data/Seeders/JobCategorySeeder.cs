using JobFinder.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data.Seeders
{
    public class JobCategorySeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<JobCategory>().HasData(
                new JobCategory { Name = "Aviation", Id = 1 },
                new JobCategory { Name = "Healthcare", Id = 2 },
                new JobCategory { Name = "IT Software Development", Id = 3 },
                new JobCategory { Name = "IT Hardware Support", Id = 4 },
                new JobCategory { Name = "Marketing", Id = 5 },
                new JobCategory { Name = "Cleaning Service", Id = 6 },
                new JobCategory { Name = "Engeneering", Id = 7 },
                new JobCategory { Name = "Advertisement", Id = 8 },
                new JobCategory { Name = "Human Resources", Id = 9 },
                new JobCategory { Name = "Tourism", Id = 10 },
                new JobCategory { Name = "Architecture", Id = 11 },
                new JobCategory { Name = "Production", Id = 12 }
                );
        }
    }
}
