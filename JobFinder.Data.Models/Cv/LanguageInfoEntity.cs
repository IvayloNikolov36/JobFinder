﻿namespace JobFinder.Data.Models.CV
{
    using JobFinder.Data.Models.Common;
    using JobFinder.Data.Models.Nomenclature;
    using System.ComponentModel.DataAnnotations;

    public class LanguageInfoEntity : BaseEntity<int>
    {
        [Required]
        public string CurriculumVitaeId { get; set; }

        public CurriculumVitaeEntity CurriculumVitae { get; set; }

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
