using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs;
using JobFinder.Web.Models.CvModels;
using System.Collections.Generic;

namespace JobFinder.Web.Models.AnonymousProfile;

public class AnonymousProfileDataViewModel : IMapFrom<AnonymousProfileDataDTO>
{
    public PersonalInfoAnonymousProfileViewModel PersonalInfo { get; set; }

    public IEnumerable<EducationViewModel> EducationInfo { get; set; }

    public IEnumerable<WorkExperienceViewModel> WorkExperienceInfo { get; set; }

    public IEnumerable<LanguageInfoViewModel> LanguagesInfo { get; set; }

    public SkillsViewModel SkillsInfo { get; set; }

    public IEnumerable<CourseInfoViewModel> CoursesInfo { get; set; }
}
