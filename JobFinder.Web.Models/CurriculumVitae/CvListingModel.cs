namespace JobFinder.Web.Models.CurriculumVitae
{
    using JobFinder.Services.Mappings;
    using JobFinder.Data.Models.CV;

    public class CvListingModel : IMapFrom<CurriculumVitae>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string PictureUrl { get; set; }
    }
}
