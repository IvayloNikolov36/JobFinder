using JobFinder.Data.Models;
using JobFinder.Data.Models.Nomenclature;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data.Seeders
{
    class EducationLevelSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<EducationLevelEntity>().HasData(
                new EducationLevelEntity { Name = "Secondary School", Id = 1 },
                new EducationLevelEntity { Name = "Proffesional", Id = 2 },
                new EducationLevelEntity { Name = "College", Id = 3 },
                new EducationLevelEntity { Name = "Bachelors Degree", Id = 4 },
                new EducationLevelEntity { Name = "Masters Degree", Id = 5 },
                new EducationLevelEntity { Name = "Doctorate", Id = 6 });
        }
    }
}
