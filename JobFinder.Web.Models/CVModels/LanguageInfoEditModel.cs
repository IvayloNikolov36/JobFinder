namespace JobFinder.Web.Models.CVModels
{
    using JobFinder.Data.Models.CV;
    using JobFinder.Data.Models.Enums;
    using JobFinder.Services.Mappings;

    public class LanguageInfoEditModel : IMapTo<LanguageInfo>
    {
        public int Id { get; set; }

        public LanguageType LanguageType { get; set; }

        public LanguageLevel Comprehension { get; set; }

        public LanguageLevel Speaking { get; set; }

        public LanguageLevel Writing { get; set; }
    }
}
