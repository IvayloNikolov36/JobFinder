using JobFinder.Data.Models.Nomenclature;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data.Seeders
{
    static class CitiesSeeder
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
                new CityEntity { Name = "Veliko Tarnovo", Id = 9 },
                new CityEntity { Name = "Dobrich", Id = 10 },
                new CityEntity { Name = "Vraca", Id = 11 },
                new CityEntity { Name = "Sliven", Id = 12 },
                new CityEntity { Name = "Kazanlak", Id = 13 },
                new CityEntity { Name = "Vidin", Id = 14 },
                new CityEntity { Name = "Nova Zagora", Id = 15 },
                new CityEntity { Name = "Shumen", Id = 16 },
                new CityEntity { Name = "Silistra", Id = 17 },
                new CityEntity { Name = "Targovishte", Id = 18 },
                new CityEntity { Name = "Pazarjik", Id = 19 },
                new CityEntity { Name = "Yambol", Id = 20 }
            );
        }
    }
}
