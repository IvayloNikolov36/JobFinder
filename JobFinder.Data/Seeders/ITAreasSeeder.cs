using JobFinder.Data.Models.Nomenclature;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data.Seeders;

static class ITAreasSeeder
{
    public static void Seed(ModelBuilder builder)
    {
        builder.Entity<ITAreaEntity>().HasData(
            new ITAreaEntity { Name = "Mobile", Id = 1 },
            new ITAreaEntity { Name = "Web", Id = 2 },
            new ITAreaEntity { Name = "Desktop", Id = 3 },
            new ITAreaEntity { Name = "Cloud", Id = 4 },
            new ITAreaEntity { Name = "Embedded", Id = 5 },
            new ITAreaEntity { Name = "QA", Id = 6 },
            new ITAreaEntity { Name = "System Administration", Id = 7 },
            new ITAreaEntity { Name = "Network Administration", Id = 8 },
            new ITAreaEntity { Name = "DevOps", Id = 9 },
            new ITAreaEntity { Name = "Security", Id = 10 },
            new ITAreaEntity { Name = "Big Data", Id = 11 },
            new ITAreaEntity { Name = "Data Science", Id = 12 },
            new ITAreaEntity { Name = "AI", Id = 13 },
            new ITAreaEntity { Name = "ML", Id = 14 },
            new ITAreaEntity { Name = "ERP", Id = 15 },
            new ITAreaEntity { Name = "Gaming", Id = 16 },
            new ITAreaEntity { Name = "IT Architecture", Id = 17 },
            new ITAreaEntity { Name = "Team Lead", Id = 18 },
            new ITAreaEntity { Name = "IT Recruitment", Id = 19 }
        );
    }
}
