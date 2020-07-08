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

        public ICollection<WorkExperiencesListingModel> WorkExperiences { get; set; }

        public ICollection<EducationsListingModel> Educations { get; set; }

        public ICollection<LanguagesListingModel> LanguagesInfo { get; set; }

        public ICollection<CoursesSertificatesListingModel> CourseCertificates { get; set; }

    }
}
