using JobFinder.Data.Models.Nomenclature;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data.Seeders
{
    public class CitiesSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<CityEntity>().HasData(
                new CityEntity { Name = "Sofia", Id = 1 },
                new CityEntity { Name = "Plovdiv", Id = 2 },
                new CityEntity { Name = "Varna", Id = 3 },
                new CityEntity { Name = "Burgas", Id = 4 },
                new CityEntity { Name = "Rouse", Id = 5 },
                new CityEntity { Name = "Stara Zagora", Id = 6 },
                new CityEntity { Name = "Pleven", Id = 7 },
                new CityEntity { Name = "Gabrovo", Id = 8 },
                new CityEntity { Name = "Veliko Tarnovo", Id = 9 }
            );
        }
    }
}
