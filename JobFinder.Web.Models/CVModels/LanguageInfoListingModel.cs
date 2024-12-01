namespace JobFinder.Web.Models.CVModels
{
    using AutoMapper;
    using JobFinder.Data.Models.CV;
    using JobFinder.Services.Mappings;
    using JobFinder.Web.Models.Common;

    public class LanguageInfoListingModel : IMapFrom<LanguageInfo>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string CurriculumVitaeId { get; set; }

        public BasicValueViewModel LanguageType { get; set; }

        public BasicValueViewModel Comprehension { get; set; }

        public BasicValueViewModel Speaking { get; set; }

        public BasicValueViewModel Writing { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<LanguageInfo, LanguageInfoListingModel>()
               .ForMember(x => x.LanguageType, m => m.MapFrom(m =>
                    new BasicValueViewModel((int)m.LanguageType, m.LanguageType.ToString())))
               .ForMember(x => x.Comprehension, m => m.MapFrom(m =>
                    new BasicValueViewModel((int)m.Comprehension, m.Comprehension.ToString())))
               .ForMember(x => x.Speaking, m => m.MapFrom(m =>
                    new BasicValueViewModel((int)m.Speaking, m.Speaking.ToString())))
               .ForMember(x => x.Writing, m => m.MapFrom(m =>
                new BasicValueViewModel((int)m.Writing, m.Writing.ToString())));
        }
    }
}
