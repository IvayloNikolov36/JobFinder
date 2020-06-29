namespace JobFinder.Web.Models.CurriculumVitae
{
    using System.ComponentModel.DataAnnotations;

    public class SkillsInputModel
    {
        public string CvId { get; set; }

        [StringLength(10000, MinimumLength = 10)]
        public string ComputerSkills { get; set; }

        [StringLength(500, MinimumLength = 10)]
        public string Skills { get; set; }

        public bool HasManagedPeople { get; set; }

        public bool HasDrivingLicense { get; set; }
    }
}
