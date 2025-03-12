using JobFinder.Data.Models.Nomenclature;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data.Seeders
{
    static class BusinessSectorsSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<BusinessSectorEntity>().HasData(
                new BusinessSectorEntity { Name = "Accounting", Id = 1 },
                new BusinessSectorEntity { Name = "Advertising", Id = 2 },
                new BusinessSectorEntity { Name = "Agriculture", Id = 3 },
                new BusinessSectorEntity { Name = "Architecture and Construction", Id = 4 },
                new BusinessSectorEntity { Name = "Arts and Entertainment", Id = 5 },
                new BusinessSectorEntity { Name = "Automotive", Id = 6 },
                new BusinessSectorEntity { Name = "Aviation", Id = 7 },
                new BusinessSectorEntity { Name = "Banks and Credit", Id = 8 },
                new BusinessSectorEntity { Name = "Chemical and Mining", Id = 9 },
                new BusinessSectorEntity { Name = "Consultancy Services", Id = 10 },
                new BusinessSectorEntity { Name = "Education", Id = 11 },
                new BusinessSectorEntity { Name = "Electronics", Id = 12 },
                new BusinessSectorEntity { Name = "Electrical", Id = 13 },
                new BusinessSectorEntity { Name = "Energy and Utilities", Id = 14 },
                new BusinessSectorEntity { Name = "Food and Beverage", Id = 15 },
                new BusinessSectorEntity { Name = "Healthcare", Id = 16 },
                new BusinessSectorEntity { Name = "Human Resources", Id = 17 },
                new BusinessSectorEntity { Name = "Information Technologies", Id = 18 },
                new BusinessSectorEntity { Name = "Insurance", Id = 19 },
                new BusinessSectorEntity { Name = "Internet", Id = 20 },
                new BusinessSectorEntity { Name = "Legal", Id = 21 },
                new BusinessSectorEntity { Name = "Manufacturing", Id = 22 },
                new BusinessSectorEntity { Name = "Machinery", Id = 23 },
                new BusinessSectorEntity { Name = "Marketing", Id = 24 },
                new BusinessSectorEntity { Name = "Media", Id = 25 },
                new BusinessSectorEntity { Name = "NGOs", Id = 26 },
                new BusinessSectorEntity { Name = "Pharmaceutical", Id = 27 },
                new BusinessSectorEntity { Name = "Public Administration", Id = 28 },
                new BusinessSectorEntity { Name = "Real Estate", Id = 29 },
                new BusinessSectorEntity { Name = "Science", Id = 30 },
                new BusinessSectorEntity { Name = "Security", Id = 31 },
                new BusinessSectorEntity { Name = "Services", Id = 32 },
                new BusinessSectorEntity { Name = "Sports and Recreation", Id = 33 },
                new BusinessSectorEntity { Name = "Telecommunications", Id = 34 },
                new BusinessSectorEntity { Name = "Textile Industry", Id = 35 },
                new BusinessSectorEntity { Name = "Tourism and Restaurants", Id = 36 },
                new BusinessSectorEntity { Name = "Trade", Id = 37 },
                new BusinessSectorEntity { Name = "Transportation and Logistics", Id = 38 },
                new BusinessSectorEntity { Name = "Other", Id = 39 });
        }
    }
}
