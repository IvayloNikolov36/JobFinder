namespace JobFinder.Web.Models.CVModels
{
    using JobFinder.Data.Models.CV;
    using JobFinder.Services.Mappings;
    using JobFinder.Web.Models.Common;
    using System;

    public class EducationViewModel : IMapFrom<EducationInfoEntity>
    {
        public int Id { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string Organization { get; set; }

        public string Location { get; set; }

        public BasicViewModel EducationLevel { get; set; }

        public string Major { get; set; }

        public string MainSubjects { get; set; }
    }
}
