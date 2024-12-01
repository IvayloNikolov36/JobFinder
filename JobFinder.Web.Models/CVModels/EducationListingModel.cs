namespace JobFinder.Web.Models.CVModels
{
    using AutoMapper;
    using JobFinder.Data.Models.CV;
    using JobFinder.Services.Mappings;
    using JobFinder.Web.Models.Common;
    using System;

    public class EducationListingModel : IMapFrom<Education>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string Organization { get; set; }

        public string Location { get; set; }

        public BasicValueViewModel EducationLevel { get; set; }

        public string Major { get; set; }

        public string MainSubjects { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Education, EducationListingModel>()
               .ForMember(x => x.EducationLevel, m => m.MapFrom(m => new BasicValueViewModel(
                   (int)m.EducationLevel,
                   m.EducationLevel.ToString())));
        }
    }
}
