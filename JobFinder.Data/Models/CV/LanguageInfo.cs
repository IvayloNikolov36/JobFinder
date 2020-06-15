namespace JobFinder.Data.Models.CV
{
    using JobFinder.Data.Models.Common;
    using JobFinder.Data.Models.Enums;

    public class LanguageInfo : BaseModel<int>
    {
        public string CurriculumVitaeId { get; set; }

        public CurriculumVitae CurriculumVitae { get; set; }

        public LanguageType LanguageType { get; set; }

        public LanguageLevel Comprehension { get; set; }

        public LanguageLevel Speaking { get; set; }

        public LanguageLevel Writing { get; set; }
    }
}
