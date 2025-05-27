using System.Collections.Generic;
using System;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.CV;

namespace JobFinder.Web.Models.CVModels;

public class MyCvDataViewModel : IMapFrom<MyCvDataDTO>
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string PictureUrl { get; set; }

    public DateTime CreatedOn { get; set; }

    public bool AnonymousProfileActivated { get; set; }

    public bool CanActivateAnonymousProfile { get; set; }

    public PersonalInfoViewModel PersonalDetails { get; set; }

    public IEnumerable<EducationViewModel> Educations { get; set; }

    public IEnumerable<WorkExperienceViewModel> WorkExperiences { get; set; }

    public IEnumerable<LanguageInfoViewModel> LanguagesInfo { get; set; }

    public SkillsViewModel Skills { get; set; }

    public IEnumerable<CourseInfoViewModel> CourseCertificates { get; set; }
}
