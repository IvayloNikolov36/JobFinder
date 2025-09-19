using System.Collections.Generic;
using System;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Cv;

namespace JobFinder.Web.Models.CvModels;

public class MyCvDataViewModel : IMapFrom<MyCvDataDTO>
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string PictureUrl { get; set; }

    public DateTime CreatedOn { get; set; }

    public string AnonymousProfileId { get; set; }

    public bool CanActivateAnonymousProfile { get; set; }

    public PersonalInfoViewModel PersonalInfo { get; set; }

    public IEnumerable<EducationViewModel> Educations { get; set; }

    public IEnumerable<WorkExperienceViewModel> WorkExperiences { get; set; }

    public IEnumerable<LanguageInfoViewModel> LanguagesInfo { get; set; }

    public SkillsViewModel Skills { get; set; }

    public IEnumerable<CourseInfoViewModel> CourseCertificates { get; set; }

    public bool ApplicationForActiveAd { get; set; }

    public bool ApprovedCvPreviewForActiveAd { get; set; }
}
