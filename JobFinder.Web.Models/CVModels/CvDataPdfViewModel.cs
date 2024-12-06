namespace JobFinder.Web.Models.CVModels
{
    using JobFinder.Services.Mappings;
    using JobFinder.Data.Models.CV;
    using System.Collections.Generic;

    public class CvDataPdfViewModel : IMapFrom<CurriculumVitaeEntity>
    {
        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public PersonalInfoViewModel PersonalDetails { get; set; }

        public SkillsViewModel Skills { get; set; }

        public ICollection<WorkExperienceViewModel> WorkExperiences { get; set; }

        public ICollection<EducationViewModel> Educations { get; set; }

        public ICollection<LanguageInfoViewModel> LanguagesInfo { get; set; }

        public ICollection<CourseCertificateViewModel> CourseCertificates { get; set; }

    }
}
