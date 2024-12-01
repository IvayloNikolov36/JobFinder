namespace JobFinder.Web.Models.CVModels
{
    using AutoMapper;
    using JobFinder.Data.Models.CV;
    using JobFinder.Services.Mappings;
    using JobFinder.Web.Models.Common;
    using System;

    public class WorkExperienceListingModel : IHaveCustomMappings
    {
        public int Id { get; set; }

        public string CurriculumVitaeId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string JobTitle { get; set; }

        public string Organization { get; set; }

        public BasicValueViewModel BusinessSector { get; set; }

        public string Location { get; set; }

        public string AditionalDetails { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<WorkExperience, WorkExperienceListingModel>()
              .ForMember(x => x.BusinessSector, m => m.MapFrom(m =>
                new BasicValueViewModel((int)m.BusinessSector, m.BusinessSector.ToString())));
        }
    }
}
