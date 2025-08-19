using JobFinder.Data.Models.Nomenclature;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data.Seeders;

public class LifecycleStatusSeeder
{
    public static void Seed(ModelBuilder builder)
    {
        builder.Entity<LifecycleStatusEntity>().HasData(
            new LifecycleStatusEntity { Id = 1, Name = "Draft" },
            new LifecycleStatusEntity { Id = 2, Name = "Active" },
            new LifecycleStatusEntity { Id = 3, Name = "Retired" }
        );
    }
}
