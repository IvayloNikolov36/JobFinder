using AutoMapper;
using JobFinder.Data.Models;
using JobFinder.Services.Mappings;
using System.Linq;

namespace JobFinder.Web.Models.UserProfile
{
    public class UserProfileDataViewModel : IHaveCustomMappings
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string PictureUrl { get; set; }

        public int CurriculumVitaesCount { get; set; }

        public int SubscriptionsCount { get; set; }

        public int ApplicationsCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<UserEntity, UserProfileDataViewModel>()
                .ForMember(vm => vm.FullName, o => o.MapFrom(e => e.FirstName + " " + e.LastName))
                // TODO: think about for picture url and phone in user profile table
                .ForMember(vm => vm.PictureUrl, o => o.MapFrom(e => e.CurriculumVitaes
                    .Select(c => c.PictureUrl)
                    .FirstOrDefault()))
                .ForMember(vm => vm.Phone, o => o.MapFrom(e => e.CurriculumVitaes
                    .Select(c => c.PersonalDetails.Phone)
                    .FirstOrDefault()))
                .ForMember(vm => vm.CurriculumVitaesCount, o => o.MapFrom(e => e.CurriculumVitaes.Count()))
                .ForMember(vm => vm.SubscriptionsCount, o => o.MapFrom(e => e.CompanySubscriptions.Count() + e.JobCategorySubscriptions.Count()))
                .ForMember(vm => vm.ApplicationsCount, o => o.MapFrom(e => e.JobAdApplications.Count()));
        }
    }
}
