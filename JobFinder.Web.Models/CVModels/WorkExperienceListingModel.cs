namespace JobFinder.Web.Models.CVModels
{
    using AutoMapper;
    using JobFinder.Data.Models.CV;
    using JobFinder.Services.Mappings;
    using System;

    public class WorkExperienceListingModel : IMapFrom<WorkExperience>, IHaveCustomMappings
    {
        public string CurriculumVitaeId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string JobTitle { get; set; }

        public string Organization { get; set; }

        public string BusinessSector { get; set; }

        public string Location { get; set; }

        public string AditionalDetails { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<WorkExperience, WorkExperienceListingModel>()
              .ForMember(x => x.BusinessSector, m => m.MapFrom(m => m.BusinessSector.ToString()));
        }
    }
}
