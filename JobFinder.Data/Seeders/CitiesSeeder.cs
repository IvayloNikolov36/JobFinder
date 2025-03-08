using JobFinder.Data.Models.Nomenclature;
using Microsoft.EntityFrameworkCore;
using System;

namespace JobFinder.Data.Seeders
{
    public class CitiesSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<CityEntity>().HasData(
                new CityEntity { Name = "Sofia", Id = 1, CreatedOn = DateTime.UtcNow },
                new CityEntity { Name = "Plovdiv", Id = 2, CreatedOn = DateTime.UtcNow },
                new CityEntity { Name = "Varna", Id = 3, CreatedOn = DateTime.UtcNow },
                new CityEntity { Name = "Burgas", Id = 4, CreatedOn = DateTime.UtcNow },
                new CityEntity { Name = "Rouse", Id = 5, CreatedOn = DateTime.UtcNow },
                new CityEntity { Name = "Stara Zagora", Id = 6, CreatedOn = DateTime.UtcNow },
                new CityEntity { Name = "Pleven", Id = 7, CreatedOn = DateTime.UtcNow },
                new CityEntity { Name = "Gabrovo", Id = 8, CreatedOn = DateTime.UtcNow },
                new CityEntity { Name = "Veliko Tarnovo", Id = 9, CreatedOn = DateTime.UtcNow }
            );
        }
    }
}
