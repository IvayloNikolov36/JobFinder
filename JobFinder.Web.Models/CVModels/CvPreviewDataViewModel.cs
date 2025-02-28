using JobFinder.Data.Models.CV;
using JobFinder.Services.Mappings;
using System.Collections.Generic;

namespace JobFinder.Web.Models.CVModels
{
    public class CvPreviewDataViewModel : IMapFrom<CurriculumVitaeEntity>
    {
        public string PictureUrl { get; set; }

        public PersonalInfoViewModel PersonalDetails { get; set; }

        public IEnumerable<EducationViewModel> Educations { get; set; }

        public IEnumerable<WorkExperienceViewModel> WorkExperiences { get; set; }

        public IEnumerable<LanguageInfoViewModel> LanguagesInfo { get; set; }

        public SkillsViewModel Skills { get; set; }

        public IEnumerable<CourseCertificateViewModel> CourseCertificates { get; set; }
    }
}
