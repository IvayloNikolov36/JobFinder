using JobFinder.Data.Models.Nomenclature;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data.Seeders;

static class ITSkillsSeeder
{
    public static void Seed(ModelBuilder builder)
    {
        builder.Entity<ITSKillEntity>().HasData(
            new ITSKillEntity {  Name = "teamwork", Id = 1 },
            new ITSKillEntity { Name = "organization skills", Id = 2 },
            new ITSKillEntity { Name = "critical thinking", Id = 3 },
            new ITSKillEntity { Name = "creativity", Id = 4 },
            new ITSKillEntity { Name = "adaptability", Id = 5 },
            new ITSKillEntity { Name = "project management", Id = 6 },
            new ITSKillEntity { Name = "leader ship", Id = 7 },
            new ITSKillEntity { Name = "presentation skills", Id = 8 }
        );
    }
}
