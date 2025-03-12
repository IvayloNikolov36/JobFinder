using JobFinder.Data.Models.Nomenclature;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data.Seeders
{
    static class JobEngagementSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<JobEngagementEntity>().HasData(
                new JobEngagementEntity { Name = "FullTime", Id = 1 },
                new JobEngagementEntity { Name = "PartTime", Id = 2 },
                new JobEngagementEntity { Name = "Permanent", Id = 3 },
                new JobEngagementEntity { Name = "Temporary", Id = 4 },
                new JobEngagementEntity { Name = "Intership", Id = 5 },
                new JobEngagementEntity { Name = "SuitableForStudents", Id = 6 },
                new JobEngagementEntity
                {
                    Name = "Suitable for candidates with no expirience",
                    Id = 7
                });
        }
    }
}
