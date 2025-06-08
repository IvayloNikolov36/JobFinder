using JobFinder.Data.Models.Nomenclature;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data.Seeders;

static class WorkplaceTypesSeeder
{
    public static void Seed(ModelBuilder builder)
    {
        builder.Entity<WorkplaceTypeEntity>()
            .HasData(
                new WorkplaceTypeEntity { Name = "Office", Id = 1 },
                new WorkplaceTypeEntity { Name = "Home", Id = 2 },
                new WorkplaceTypeEntity { Name = "Hybrid", Id = 3 });
    }
}
