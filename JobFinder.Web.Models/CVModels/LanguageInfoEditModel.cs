namespace JobFinder.Web.Models.CVModels
{
    using AutoMapper;
    using JobFinder.Data.Models.CV;
    using JobFinder.Data.Models.Enums;
    using JobFinder.Services.Mappings;

    public class LanguageInfoEditModel : IHaveCustomMappings
    {
        public int Id { get; set; }

        public int LanguageType { get; set; }

        public int Comprehension { get; set; }

        public int Speaking { get; set; }

        public int Writing { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<LanguageInfoEditModel, LanguageInfo>()
                .ForMember(e => e.LanguageType, o => o.MapFrom(vm => (LanguageType)vm.LanguageType))
                .ForMember(e => e.Comprehension, o => o.MapFrom(vm => (LanguageLevel)vm.Comprehension))
                .ForMember(e => e.Speaking, o => o.MapFrom(vm => (LanguageLevel)vm.Speaking))
                .ForMember(e => e.Writing, o => o.MapFrom(vm => (LanguageLevel)vm.Writing));
        }
    }
}
