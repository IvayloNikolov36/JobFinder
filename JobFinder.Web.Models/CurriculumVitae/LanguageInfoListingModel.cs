namespace JobFinder.Web.Models.CurriculumVitae
{
    using AutoMapper;
    using JobFinder.Data.Models.CV;
    using JobFinder.Services.Mappings;

    public class LanguageInfoListingModel : IMapFrom<LanguageInfo>, IHaveCustomMappings
    {
        public string CurriculumVitaeId { get; set; }

        public string LanguageType { get; set; }

        public string Comprehension { get; set; }

        public string Speaking { get; set; }

        public string Writing { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<LanguageInfo, LanguageInfoListingModel>()
               .ForMember(x => x.LanguageType, m => m.MapFrom(m => m.LanguageType.ToString()))
               .ForMember(x => x.Comprehension, m => m.MapFrom(m => m.Comprehension.ToString()))
               .ForMember(x => x.Speaking, m => m.MapFrom(m => m.Speaking.ToString()))
               .ForMember(x => x.Writing, m => m.MapFrom(m => m.Writing.ToString()));
        }
    }
}
