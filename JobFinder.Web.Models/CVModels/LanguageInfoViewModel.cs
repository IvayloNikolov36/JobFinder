namespace JobFinder.Web.Models.CVModels
{
    using JobFinder.Data.Models.CV;
    using JobFinder.Services.Mappings;
    using JobFinder.Web.Models.Common;

    public class LanguageInfoViewModel : IMapFrom<LanguageInfo>
    {
        public int Id { get; set; }

        public BasicViewModel LanguageType { get; set; }

        public BasicViewModel ComprehensionLevel { get; set; }

        public BasicViewModel SpeakingLevel { get; set; }

        public BasicViewModel WritingLevel { get; set; }
    }
}
