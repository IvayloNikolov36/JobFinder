using JobFinder.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data.Seeders
{
    public class BusinessSectorsSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<BusinessSector>().HasData(
                new BusinessSector { Name = "Accounting", Id = 1 },
                new BusinessSector { Name = "Advertising", Id = 2 },
                new BusinessSector { Name = "Agriculture", Id = 3 },
                new BusinessSector { Name = "Architecture and Construction", Id = 4 },
                new BusinessSector { Name = "Arts and Entertainment", Id = 5 },
                new BusinessSector { Name = "Automotive", Id = 6 },
                new BusinessSector { Name = "Aviation", Id = 7 },
                new BusinessSector { Name = "Banks and Credit", Id = 8 },
                new BusinessSector { Name = "Chemical and Mining", Id = 9 },
                new BusinessSector { Name = "Consultancy Services", Id = 10 },
                new BusinessSector { Name = "Education", Id = 11 },
                new BusinessSector { Name = "Electronics", Id = 12 },
                new BusinessSector { Name = "Electrical", Id = 13 },
                new BusinessSector { Name = "Energy and Utilities", Id = 14 },
                new BusinessSector { Name = "Food and Beverage", Id = 15 },
                new BusinessSector { Name = "Healthcare", Id = 16 },
                new BusinessSector { Name = "Human Resources", Id = 17 },
                new BusinessSector { Name = "Information Technologies", Id = 18 },
                new BusinessSector { Name = "Insurance", Id = 19 },
                new BusinessSector { Name = "Internet", Id = 20 },
                new BusinessSector { Name = "Legal", Id = 21 },
                new BusinessSector { Name = "Manufacturing", Id = 22 },
                new BusinessSector { Name = "Machinery", Id = 23 },
                new BusinessSector { Name = "Marketing", Id = 24 },
                new BusinessSector { Name = "Media", Id = 25 },
                new BusinessSector { Name = "NGOs", Id = 26 },
                new BusinessSector { Name = "Pharmaceutical", Id = 27 },
                new BusinessSector { Name = "Public Administration", Id = 28 },
                new BusinessSector { Name = "Real Estate", Id = 29 },
                new BusinessSector { Name = "Science", Id = 30 },
                new BusinessSector { Name = "Security", Id = 31 },
                new BusinessSector { Name = "Services", Id = 32 },
                new BusinessSector { Name = "Sports and Recreation", Id = 33 },
                new BusinessSector { Name = "Telecommunications", Id = 34 },
                new BusinessSector { Name = "Textile Industry", Id = 35 },
                new BusinessSector { Name = "Tourism and Restaurants", Id = 36 },
                new BusinessSector { Name = "Trade", Id = 37 },
                new BusinessSector { Name = "Transportation and Logistics", Id = 38 },
                new BusinessSector { Name = "Other", Id = 39 });
        }
    }
}
