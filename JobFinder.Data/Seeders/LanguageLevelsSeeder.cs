using JobFinder.Data.Models.Nomenclature;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data.Seeders
{
    static class LanguageLevelsSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<LanguageLevelEntity>().HasData(
                new LanguageLevelEntity { Name = "Beginner", Id = 1 },
                new LanguageLevelEntity { Name = "Intermediate", Id = 2 },
                new LanguageLevelEntity { Name = "Fluent", Id = 3 });
        }
    }
}
