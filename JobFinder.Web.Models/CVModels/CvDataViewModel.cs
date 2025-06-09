
namespace JobFinder.Web.Models.CVModels
{
    using AutoMapper;
    using JobFinder.Data.Models.CV;
    using JobFinder.Services.Mappings;
    using System;
    using System.Collections.Generic;

    public class CvDataViewModel : IHaveCustomMappings
    {
        public string Id { get; set; }

        public string OwnerId { get; set; }

        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public PersonalInfoViewModel PersonalInfo { get; set; }

        public IEnumerable<EducationViewModel> Educations { get; set; }

        public IEnumerable<WorkExperienceViewModel> WorkExperiences { get; set; }

        public IEnumerable<LanguageInfoViewModel> LanguagesInfo { get; set; }

        public SkillsViewModel Skills { get; set; }

        public IEnumerable<CourseInfoViewModel> CourseCertificates { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CurriculumVitaeEntity, CvDataViewModel>()
                .ForMember(model => model.OwnerId, m => m.MapFrom(e => e.UserId));
        }
    }
}
