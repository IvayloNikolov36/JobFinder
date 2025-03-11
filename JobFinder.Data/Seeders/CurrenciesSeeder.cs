using JobFinder.Data.Models.Nomenclature;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data.Seeders
{
    public class CurrenciesSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<CurrencyEntity>().HasData(
                new CurrencyEntity { Id = 1, Name = "BGN" },
                new CurrencyEntity { Id = 2, Name = "EUR" },
                new CurrencyEntity { Id = 3, Name = "USD" }
            );
        }
    }
}
