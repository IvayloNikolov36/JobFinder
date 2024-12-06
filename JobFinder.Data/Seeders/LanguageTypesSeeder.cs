using JobFinder.Data.Models;
using JobFinder.Data.Models.Nomenclature;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data.Seeders
{
    public class LanguageTypesSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<LanguageTypeEntity>().HasData(
                new LanguageTypeEntity { Name = "Arabic", Id = 1 },
                new LanguageTypeEntity { Name = "Bulgarian", Id = 2 },
                new LanguageTypeEntity { Name = "Chinese", Id = 3 },
                new LanguageTypeEntity { Name = "Czech", Id = 4 },
                new LanguageTypeEntity { Name = "Danish", Id = 5 },
                new LanguageTypeEntity { Name = "Dutch", Id = 6 },
                new LanguageTypeEntity { Name = "English", Id = 7 },
                new LanguageTypeEntity { Name = "Estonian", Id = 8 },
                new LanguageTypeEntity { Name = "Finnish", Id = 9 },
                new LanguageTypeEntity { Name = "French", Id = 10 },
                new LanguageTypeEntity { Name = "German", Id = 11 },
                new LanguageTypeEntity { Name = "Greek", Id = 12 },
                new LanguageTypeEntity { Name = "Hindi", Id = 13 },
                new LanguageTypeEntity { Name = "Hungarian", Id = 14 },
                new LanguageTypeEntity { Name = "Italian", Id = 15 },
                new LanguageTypeEntity { Name = "Japanese", Id = 16 },
                new LanguageTypeEntity { Name = "Korean", Id = 17 },
                new LanguageTypeEntity { Name = "Latvian", Id = 18 },
                new LanguageTypeEntity { Name = "Lithuanian", Id = 19 },
                new LanguageTypeEntity { Name = "Norwegian", Id = 20 },
                new LanguageTypeEntity { Name = "Polish", Id = 21 },
                new LanguageTypeEntity { Name = "Portuguese", Id = 22 },
                new LanguageTypeEntity { Name = "Romanian", Id = 23 },
                new LanguageTypeEntity { Name = "Russian", Id = 24 },
                new LanguageTypeEntity { Name = "Serbian", Id = 25 },
                new LanguageTypeEntity { Name = "Slovak", Id = 26 },
                new LanguageTypeEntity { Name = "Slovene", Id = 27 },
                new LanguageTypeEntity { Name = "Spanish", Id = 28 },
                new LanguageTypeEntity { Name = "Swedish", Id = 29 },
                new LanguageTypeEntity { Name = "Turkish", Id = 30 },
                new LanguageTypeEntity { Name = "Ukrainian", Id = 31 });
        }
    }
}
