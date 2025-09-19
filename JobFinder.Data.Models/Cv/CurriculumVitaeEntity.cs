using JobFinder.Data.Models.AnonymousProfile;
using JobFinder.Data.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Data.Models.Cv
{    
    public partial class CurriculumVitaeEntity : BaseEntity<string>
    {
        public CurriculumVitaeEntity()
        {
            this.Id = Guid.NewGuid().ToString();
            this.WorkExperiences = [];
            this.Educations = [];
            this.LanguagesInfo = [];
            this.CourseCertificates = [];
            this.CvPreviewRequests = [];
            this.JobAdApplications = [];
        }

        public string UserId { get; set; }
        public UserEntity User { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [Url]
        public string PictureUrl { get; set; }

        public AnonymousProfileEntity AnonymousProfile { get; set; }

        public PersonalInfoEntity PersonalInfo { get; set; }

        public SkillsInfoEntity Skills { get; set; }

        public ICollection<WorkExperienceInfoEntity> WorkExperiences { get; set; }

        public ICollection<EducationInfoEntity> Educations { get; set; }

        public ICollection<LanguageInfoEntity> LanguagesInfo { get; set; }

        public ICollection<CourseCertificateEntity> CourseCertificates { get; set; }

        public ICollection<CvPreviewRequestEntity> CvPreviewRequests { get; set; }

        public ICollection<JobAdApplicationEntity> JobAdApplications { get; set; }
    }
}
