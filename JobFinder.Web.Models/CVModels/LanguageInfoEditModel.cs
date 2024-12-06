namespace JobFinder.Web.Models.CVModels
{
    using JobFinder.Data.Models.CV;
    using JobFinder.Services.Mappings;

    public class LanguageInfoEditModel : IMapTo<LanguageInfoEntity>
    {
        public int Id { get; set; }

        public int LanguageTypeId { get; set; }

        public int ComprehensionLevelId { get; set; }

        public int SpeakingLevelId { get; set; }

        public int WritingLevelId { get; set; }
    }
}
