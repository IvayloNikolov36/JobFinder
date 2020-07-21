namespace JobFinder.Data.Models.CV
{
    using JobFinder.Data.Models.Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CurriculumVitae : BaseModel<string>
    {
        public CurriculumVitae()
        {
            this.Id = Guid.NewGuid().ToString();
            this.WorkExperiences = new HashSet<WorkExperience>();
            this.Educations = new HashSet<Education>();
            this.LanguagesInfo = new HashSet<LanguageInfo>();
            this.CourseCertificates = new HashSet<CourseCertificate>();
        }

        public string UserId { get; set; }

        public User User { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [Url]
        public string PictureUrl { get; set; }

        public byte[] Data { get; set; }

        public PersonalDetails PersonalDetails { get; set; }

        public Skill Skills { get; set; }

        public ICollection<WorkExperience> WorkExperiences { get; set; }

        public ICollection<Education> Educations { get; set; }

        public ICollection<LanguageInfo> LanguagesInfo { get; set; }

        public ICollection<CourseCertificate> CourseCertificates { get; set; }
    }
}
