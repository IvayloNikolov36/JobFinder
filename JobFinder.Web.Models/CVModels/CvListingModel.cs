namespace JobFinder.Web.Models.CVModels
{
    using JobFinder.Services.Mappings;
    using JobFinder.Data.Models.CV;
    using System;

    public class CvListingModel : IMapFrom<CurriculumVitaeEntity>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
