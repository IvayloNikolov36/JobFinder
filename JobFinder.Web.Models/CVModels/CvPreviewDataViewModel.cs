using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Cv;
using System.Collections.Generic;

namespace JobFinder.Web.Models.CvModels
{
    public class CvPreviewDataViewModel : IMapFrom<CvPreviewDataDTO>
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
