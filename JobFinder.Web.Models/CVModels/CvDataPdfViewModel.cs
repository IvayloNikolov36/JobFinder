using JobFinder.Data.Models.Cv;
using JobFinder.Services.Mappings;
using System.Collections.Generic;

namespace JobFinder.Web.Models.CvModels
{
    public class CvDataPdfViewModel : IMapFrom<CurriculumVitaeEntity>
    {
        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public PersonalInfoViewModel PersonalInfo { get; set; }

        public SkillsViewModel Skills { get; set; }

        public ICollection<WorkExperienceViewModel> WorkExperiences { get; set; }

        public ICollection<EducationViewModel> Educations { get; set; }

        public ICollection<LanguageInfoViewModel> LanguagesInfo { get; set; }

        public ICollection<CourseInfoViewModel> CourseCertificates { get; set; }

    }
}
