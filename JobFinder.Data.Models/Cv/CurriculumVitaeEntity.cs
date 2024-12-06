using JobFinder.Data.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Data.Models.CV
{    
    public class CurriculumVitaeEntity : BaseEntity<string>
    {
        public CurriculumVitaeEntity()
        {
            this.Id = Guid.NewGuid().ToString();
            this.WorkExperiences = new HashSet<WorkExperienceInfoEntity>();
            this.Educations = new HashSet<EducationInfoEntity>();
            this.LanguagesInfo = new HashSet<LanguageInfoEntity>();
            this.CourseCertificates = new HashSet<CourseCertificateEntity>();
        }

        public string UserId { get; set; }

        public UserEntity User { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [Url]
        public string PictureUrl { get; set; }

        public byte[] Data { get; set; }

        public PersonalInfoEntity PersonalDetails { get; set; }

        public SkillsInfoEntity Skills { get; set; }

        public ICollection<WorkExperienceInfoEntity> WorkExperiences { get; set; }

        public ICollection<EducationInfoEntity> Educations { get; set; }

        public ICollection<LanguageInfoEntity> LanguagesInfo { get; set; }

        public ICollection<CourseCertificateEntity> CourseCertificates { get; set; }
    }
}
