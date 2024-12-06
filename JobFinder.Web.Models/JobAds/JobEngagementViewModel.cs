namespace JobFinder.Web.Models.JobAds
{
    using JobFinder.Data.Models.Nomenclature;
    using JobFinder.Services.Mappings;

    public class JobEngagementViewModel : IMapFrom<JobEngagementEntity>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
