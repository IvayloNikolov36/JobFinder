using JobFinder.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data.Seeders
{
    public class LanguageLevelsSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<LanguageLevel>().HasData(
                new LanguageLevel { Name = "Beginner", Id = 1 },
                new LanguageLevel { Name = "Intermediate", Id = 2 },
                new LanguageLevel { Name = "Fluent", Id = 3 }
                );
        }
    }
}
