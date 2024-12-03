using JobFinder.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data.Seeders
{
    public class LanguageTypesSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<LanguageType>().HasData(
                new LanguageType { Name = "Arabic", Id = 1 },
                new LanguageType { Name = "Bulgarian", Id = 2 },
                new LanguageType { Name = "Chinese", Id = 3 },
                new LanguageType { Name = "Czech", Id = 4 },
                new LanguageType { Name = "Danish", Id = 5 },
                new LanguageType { Name = "Dutch", Id = 6 },
                new LanguageType { Name = "English", Id = 7 },
                new LanguageType { Name = "Estonian", Id = 8 },
                new LanguageType { Name = "Finnish", Id = 9 },
                new LanguageType { Name = "French", Id = 10 },
                new LanguageType { Name = "German", Id = 11 },
                new LanguageType { Name = "Greek", Id = 12 },
                new LanguageType { Name = "Hindi", Id = 13 },
                new LanguageType { Name = "Hungarian", Id = 14 },
                new LanguageType { Name = "Italian", Id = 15 },
                new LanguageType { Name = "Japanese", Id = 16 },
                new LanguageType { Name = "Korean", Id = 17 },
                new LanguageType { Name = "Latvian", Id = 18 },
                new LanguageType { Name = "Lithuanian", Id = 19 },
                new LanguageType { Name = "Norwegian", Id = 20 },
                new LanguageType { Name = "Polish", Id = 21 },
                new LanguageType { Name = "Portuguese", Id = 22 },
                new LanguageType { Name = "Romanian", Id = 23 },
                new LanguageType { Name = "Russian", Id = 24 },
                new LanguageType { Name = "Serbian", Id = 25 },
                new LanguageType { Name = "Slovak", Id = 26 },
                new LanguageType { Name = "Slovene", Id = 27 },
                new LanguageType { Name = "Spanish", Id = 28 },
                new LanguageType { Name = "Swedish", Id = 29 },
                new LanguageType { Name = "Turkish", Id = 30 },
                new LanguageType { Name = "Ukrainian", Id = 31 }
                );
        }
    }
}
