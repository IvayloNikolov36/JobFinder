namespace JobFinder.Web.Models.CVModels
{
    using JobFinder.Data.Models.CV;
    using JobFinder.Services.Mappings;

    public class LanguageInfoInputModel : IMapTo<LanguageInfoEntity>
    {
        public int LanguageTypeId { get; set; }

        public int ComprehensionLevelId { get; set; }

        public int SpeakingLevelId { get; set; }

        public int WritingLevelId { get; set; }
    }
}
