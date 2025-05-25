using JobFinder.Data.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Data.Models.CV
{    
    public partial class CurriculumVitaeEntity : BaseEntity<string>
    {
        public CurriculumVitaeEntity()
        {
            this.Id = Guid.NewGuid().ToString();
            this.WorkExperiences = new List<WorkExperienceInfoEntity>();
            this.Educations = new List<EducationInfoEntity>();
            this.LanguagesInfo = new List<LanguageInfoEntity>();
            this.CourseCertificates = new List<CourseCertificateEntity>();
        }

        public string UserId { get; set; }
        public UserEntity User { get; set; }

        public bool AnonymousProfileActivated { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [Url]
        public string PictureUrl { get; set; }

        public byte[] Data { get; set; }

        // TODO: rename it to PersonalInfo
        public PersonalInfoEntity PersonalDetails { get; set; }

        public SkillsInfoEntity Skills { get; set; }

        public ICollection<WorkExperienceInfoEntity> WorkExperiences { get; set; }

        public ICollection<EducationInfoEntity> Educations { get; set; }

        public ICollection<LanguageInfoEntity> LanguagesInfo { get; set; }

        public ICollection<CourseCertificateEntity> CourseCertificates { get; set; }
    }
}
