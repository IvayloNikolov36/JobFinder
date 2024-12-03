using JobFinder.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data.Seeders
{
    class EducationLevelSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<EducationLevel>().HasData(
                new EducationLevel { Name = "Secondary School", Id = 1 },
                new EducationLevel { Name = "Proffesional", Id = 2 },
                new EducationLevel { Name = "College", Id = 3 },
                new EducationLevel { Name = "Bachelors Degree", Id = 4 },
                new EducationLevel { Name = "Masters Degree", Id = 5 },
                new EducationLevel { Name = "Doctorate", Id = 6 });
        }
    }
}
