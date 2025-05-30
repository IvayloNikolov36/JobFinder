using JobFinder.Common.DataAnnotations;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.CV;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Web.Models.CVModels
{
    public class CVCreateInputModel : IMapTo<CVCreateDTO>
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [Url]
        public string PictureUrl { get; set; }

        [Required]
        public PersonalDetailsInputModel PersonalDetails { get; set; }

        [Required]
        [NotEmptyCollection]
        public IEnumerable<EducationInputModel> Educations { get; set; }

        [Required]
        [NotEmptyCollection]
        public IEnumerable<WorkExperienceInputModel> WorkExperiences { get; set; }

        [Required]
        [NotEmptyCollection]
        public IEnumerable<LanguageInfoInputModel> LanguagesInfo { get; set; }

        [Required]
        public SkillsInputModel Skills { get; set; }

        public IEnumerable<CourseSertificateInputModel> CourseCertificates { get; set; } = [];
    }
}
