namespace JobFinder.Web.Models.CVModels
{
    using JobFinder.Data.Models.CV;
    using JobFinder.Services.Mappings;
    using JobFinder.Web.Models.Common;
    using System;

    public class WorkExperienceViewModel : IMapFrom<WorkExperienceInfoEntity>
    {
        public int Id { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string JobTitle { get; set; }

        public string Organization { get; set; }

        public BasicViewModel BusinessSector { get; set; }

        public string Location { get; set; }

        public string AdditionalDetails { get; set; }
    }
}
