using JobFinder.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data.Seeders
{
    public class JobEngagementSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<JobEngagement>().HasData(
                new JobEngagement { Name = "FullTime", Id = 1 },
                new JobEngagement { Name = "PartTime", Id = 2 },
                new JobEngagement { Name = "Permanent", Id = 3 },
                new JobEngagement { Name = "Temporary", Id = 4 },
                new JobEngagement { Name = "Intership", Id = 5 },
                new JobEngagement { Name = "SuitableForStudents", Id = 6 },
                new JobEngagement
                {
                    Name = "Suitable for candidates with no expirience",
                    Id = 7
                });
        }
    }
}
