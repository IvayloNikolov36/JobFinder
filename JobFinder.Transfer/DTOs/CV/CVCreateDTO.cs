﻿using JobFinder.Transfer.Common;

namespace JobFinder.Transfer.DTOs.CV;

public class CVCreateDTO : IUniquelyIdentified<string>
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string PictureUrl { get; set; }

    public PersonalInfoInputDTO PersonalDetails { get; set; }

    public IEnumerable<EducationInfoInputDTO> Educations { get; set; }

    public IEnumerable<WorkExperienceInfoInputDTO> WorkExperiences { get; set; }

    public IEnumerable<LanguageInfoInputDTO> LanguagesInfo { get; set; }

    public SkillsInfoInputDTO Skills { get; set; }

    public IEnumerable<CourseCertificateInputDTO> CourseCertificates { get; set; } = [];

    public Guid UniqueIdentificator { get; set; }
}
