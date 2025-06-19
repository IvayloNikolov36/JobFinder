using JobFinder.Data.Models.Nomenclature;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data.Seeders;

static class ITAreasSeeder
{
    public static void Seed(ModelBuilder builder)
    {
        builder.Entity<ItAreaEntity>().HasData(
            new ItAreaEntity { Name = "Mobile", Id = 1 },
            new ItAreaEntity { Name = "Web", Id = 2 },
            new ItAreaEntity { Name = "Desktop", Id = 3 },
            new ItAreaEntity { Name = "Cloud", Id = 4 },
            new ItAreaEntity { Name = "Embedded", Id = 5 },
            new ItAreaEntity { Name = "QA", Id = 6 },
            new ItAreaEntity { Name = "System Administration", Id = 7 },
            new ItAreaEntity { Name = "Network Administration", Id = 8 },
            new ItAreaEntity { Name = "DevOps", Id = 9 },
            new ItAreaEntity { Name = "Security", Id = 10 },
            new ItAreaEntity { Name = "Big Data", Id = 11 },
            new ItAreaEntity { Name = "Data Science", Id = 12 },
            new ItAreaEntity { Name = "AI", Id = 13 },
            new ItAreaEntity { Name = "ML", Id = 14 },
            new ItAreaEntity { Name = "ERP", Id = 15 },
            new ItAreaEntity { Name = "Gaming", Id = 16 },
            new ItAreaEntity { Name = "IT Architecture", Id = 17 },
            new ItAreaEntity { Name = "Team Lead", Id = 18 },
            new ItAreaEntity { Name = "IT Recruitment", Id = 19 }
        );
    }
}
