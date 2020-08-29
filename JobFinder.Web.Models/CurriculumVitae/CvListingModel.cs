namespace JobFinder.Web.Models.CurriculumVitae
{
    using JobFinder.Services.Mappings;
    using JobFinder.Data.Models.CV;
    using System;

    public class CvListingModel : IMapFrom<CurriculumVitae>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
