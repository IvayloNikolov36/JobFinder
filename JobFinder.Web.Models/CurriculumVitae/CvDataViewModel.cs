namespace JobFinder.Web.Models.CurriculumVitae
{
    using JobFinder.Services.Mappings;
    using JobFinder.Data.Models.CV;
    using System.Collections.Generic;

    public class CvDataViewModel : IMapFrom<CurriculumVitae>
    {
        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public PersonalDetailsViewModel PersonalDetails { get; set; }

        public SkillsViewModel Skills { get; set; }

        public ICollection<WorkExperienceListingModel> WorkExperiences { get; set; }

        public ICollection<EducationListingModel> Educations { get; set; }

        public ICollection<LanguageInfoListingModel> LanguagesInfo { get; set; }

        public ICollection<CourseSertificateListingModel> CourseCertificates { get; set; }

    }
}
