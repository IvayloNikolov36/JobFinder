using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.Nomenclature;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Data.Models.Cv
{
    public partial class LanguageInfoEntity : BaseEntity<int>
    {
        [Required]
        public string CvId { get; set; }

        public CurriculumVitaeEntity Cv { get; set; }

        public bool? IncludeInAnonymousProfile { get; set; }

        [Required]
        public int LanguageTypeId { get; set; }

        public LanguageTypeEntity LanguageType { get; set; }

        public int ComprehensionLevelId { get; set; }
        public LanguageLevelEntity ComprehensionLevel { get; set; }

        public int SpeakingLevelId { get; set; }
        public LanguageLevelEntity SpeakingLevel { get; set; }

        public int WritingLevelId { get; set; }
        public LanguageLevelEntity WritingLevel { get; set; }
    }
}
