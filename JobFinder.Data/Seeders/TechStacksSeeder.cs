using JobFinder.Data.Models.Nomenclature;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data.Seeders;

static class TechStacksSeeder
{
    public static void Seed(ModelBuilder builder)
    {
        builder.Entity<TechStackEntity>().HasData(
            new TechStackEntity { Name = ".Net", Id = 1 },
            new TechStackEntity { Name = "C#", Id = 2 },
            new TechStackEntity { Name = "Java", Id = 3 },
            new TechStackEntity { Name = "Typescript", Id = 4 },
            new TechStackEntity { Name = "JavaScript", Id = 5 },
            new TechStackEntity { Name = "PHP", Id = 6 },
            new TechStackEntity { Name = "Python", Id = 7 },
            new TechStackEntity { Name = "Go", Id = 8 },
            new TechStackEntity { Name = "Angular", Id = 9 },
            new TechStackEntity { Name = "React", Id = 10 },
            new TechStackEntity { Name = "View", Id = 11 },
            new TechStackEntity { Name = "Flutter", Id = 12 },
            new TechStackEntity { Name = "React Native", Id = 13 },
            new TechStackEntity { Name = ".NET MAUI", Id = 14 },
            new TechStackEntity { Name = "Blazor", Id = 15 },
            new TechStackEntity { Name = "C/C++", Id = 16 },
            new TechStackEntity { Name = "T SQL", Id = 17 },
            new TechStackEntity { Name = "MySQL", Id = 18 },
            new TechStackEntity { Name = "MongoDB", Id = 19 },
            new TechStackEntity { Name = "Selenium", Id = 20 },
            new TechStackEntity { Name = "Spring", Id = 21 },
            new TechStackEntity { Name = "ASP .NET", Id = 22 },
            new TechStackEntity { Name = "Jango", Id = 23 },
            new TechStackEntity { Name = "AWS", Id = 24 },
            new TechStackEntity { Name = "Azure", Id = 25 },
            new TechStackEntity { Name = "GCP", Id = 26 },
            new TechStackEntity { Name = "NUnit", Id = 27 },
            new TechStackEntity { Name = "HTML", Id = 28 },
            new TechStackEntity { Name = "CSS", Id = 29 },
            new TechStackEntity { Name = "Bootstrap", Id = 30 },
            new TechStackEntity { Name = "TailWind", Id = 31 }
        );
    }
}