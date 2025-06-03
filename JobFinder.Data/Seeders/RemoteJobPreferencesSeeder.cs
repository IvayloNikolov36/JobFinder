using JobFinder.Data.Models.Nomenclature;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data.Seeders;

static class RemoteJobPreferencesSeeder
{
    public static void Seed(ModelBuilder builder)
    {
        builder.Entity<RemoteJobPreferenceEntity>().HasData(
            new RemoteJobPreferenceEntity { Name = "only remote job offers", Id = 1 },
            new RemoteJobPreferenceEntity { Name = "interested in remote job offers", Id = 2 },
            new RemoteJobPreferenceEntity { Name = "not interested in remote job offers", Id = 3 }                
        );
    }
}
