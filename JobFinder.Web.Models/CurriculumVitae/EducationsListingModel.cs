namespace JobFinder.Web.Models.CurriculumVitae
{
    using AutoMapper;
    using JobFinder.Data.Models.CV;
    using JobFinder.Services.Mappings;
    using System;

    public class EducationsListingModel : IMapFrom<Education>, IHaveCustomMappings
    {
        public DateTime FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string Organization { get; set; }

        public string Location { get; set; }

        public string EducationLevel { get; set; }

        public string Major { get; set; }

        public string MainSubjects { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Education, EducationsListingModel>()
               .ForMember(x => x.EducationLevel, m => m.MapFrom(m => m.EducationLevel.ToString()));
        }
    }
}
