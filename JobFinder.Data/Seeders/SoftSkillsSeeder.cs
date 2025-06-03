using JobFinder.Data.Models.Nomenclature;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data.Seeders;

static class SoftSkillsSeeder
{
    public static void Seed(ModelBuilder builder)
    {
        builder.Entity<SoftSKillEntity>().HasData(
            new SoftSKillEntity {  Name = "teamwork", Id = 1 },
            new SoftSKillEntity { Name = "organization skills", Id = 2 },
            new SoftSKillEntity { Name = "critical thinking", Id = 3 },
            new SoftSKillEntity { Name = "creativity", Id = 4 },
            new SoftSKillEntity { Name = "adaptability", Id = 5 },
            new SoftSKillEntity { Name = "project management", Id = 6 },
            new SoftSKillEntity { Name = "leader ship", Id = 7 },
            new SoftSKillEntity { Name = "presentation skills", Id = 8 }
        );
    }
}
