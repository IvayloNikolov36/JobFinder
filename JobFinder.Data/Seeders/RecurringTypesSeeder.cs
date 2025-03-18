using JobFinder.Data.Models.Nomenclature;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data.Seeders
{
    static class RecurringTypesSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<RecurringTypeEntity>().HasData(
                new RecurringTypeEntity { Id = 1, Name = "Daily" },
                new RecurringTypeEntity { Id = 2, Name = "Weekly" },
                new RecurringTypeEntity { Id = 3, Name = "Monthly" }
            );
        }
    }
}
