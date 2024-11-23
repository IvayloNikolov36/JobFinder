namespace JobFinder.Web.Models.CVModels
{
    using JobFinder.Data.Models.Enums;

    public class LanguageInfoEditModel
    {
        public LanguageType LanguageType { get; set; }

        public LanguageLevel Comprehension { get; set; }

        public LanguageLevel Speaking { get; set; }

        public LanguageLevel Writing { get; set; }
    }
}
