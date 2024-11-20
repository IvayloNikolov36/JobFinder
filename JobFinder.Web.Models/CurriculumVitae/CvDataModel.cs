
namespace JobFinder.Web.Models.CurriculumVitae
{
    using AutoMapper;
    using JobFinder.Data.Models.CV;
    using JobFinder.Services.Mappings;
    using System;
    using System.Collections.Generic;

    public class CvDataModel : IMapFrom<CurriculumVitae>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string OwnerId { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public PersonalDetailsViewModel PersonalDetails { get; set; }

        public IEnumerable<LanguageInfoListingModel> LanguagesInfo { get; set; }

        public IEnumerable<WorkExperienceListingModel> WorkExperiences { get; set; }

        public IEnumerable<EducationListingModel> Educations { get; set; }

        public IEnumerable<CourseCertificateListingModel> CourseCertificates { get; set; }
        
        public SkillsViewModel Skills { get; set; }


        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CurriculumVitae, CvDataModel>()
                .ForMember(model => model.OwnerId, m => m.MapFrom(e => e.UserId));
        }
    }
}
