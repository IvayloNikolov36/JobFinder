namespace JobFinder.Data.Models.CV
{
    using JobFinder.Data.Models.Common;
    using System.ComponentModel.DataAnnotations;

    public class LanguageInfo : BaseModel<int>
    {
        [Required]
        public string CurriculumVitaeId { get; set; }

        public CurriculumVitae CurriculumVitae { get; set; }

        [Required]
        public int LanguageTypeId { get; set; }

        public LanguageType LanguageType { get; set; }

        public int ComprehensionLevelId { get; set; }
        public LanguageLevel ComprehensionLevel { get; set; }

        public int SpeakingLevelId { get; set; }
        public LanguageLevel SpeakingLevel { get; set; }

        public int WritingLevelId { get; set; }
        public LanguageLevel WritingLevel { get; set; }
    }
}
