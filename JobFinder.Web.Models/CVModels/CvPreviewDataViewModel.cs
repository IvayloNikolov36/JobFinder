using JobFinder.Data.Models.Cv;
using JobFinder.Services.Mappings;
using System.Collections.Generic;

namespace JobFinder.Web.Models.CvModels
{
    public class CvPreviewDataViewModel : IMapFrom<CurriculumVitaeEntity>
    {
        public string PictureUrl { get; set; }

        public PersonalInfoViewModel PersonalInfo { get; set; }

        public IEnumerable<EducationViewModel> Educations { get; set; }

        public IEnumerable<WorkExperienceViewModel> WorkExperiences { get; set; }

        public IEnumerable<LanguageInfoViewModel> LanguagesInfo { get; set; }

        public SkillsViewModel Skills { get; set; }

        public IEnumerable<CourseInfoViewModel> CourseCertificates { get; set; }
    }
}
