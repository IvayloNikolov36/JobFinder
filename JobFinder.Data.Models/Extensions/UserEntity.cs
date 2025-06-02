using AutoMapper;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.User;
using System.Linq;

namespace JobFinder.Data.Models;

public partial class UserEntity : IHaveCustomMappings
{
    public void CreateMappings(IProfileExpression configuration)
    {
        configuration.CreateMap<UserEntity, UserProfileDataDTO>()
            .ForMember(dto => dto.FullName, o => o.MapFrom(e => e.FirstName + " " + e.LastName))
            // TODO: think about for picture url in user profile table
            .ForMember(dto => dto.PictureUrl, o => o.MapFrom(e => e.CurriculumVitaes
                .Select(c => c.PictureUrl)
                .FirstOrDefault()))
            .ForMember(dto => dto.Phone, o => o.MapFrom(e => e.CurriculumVitaes
                .Select(c => c.PersonalDetails.Phone)
                .FirstOrDefault()))
            .ForMember(dto => dto.CurriculumVitaesCount, o => o.MapFrom(e => e.CurriculumVitaes.Count()))
            .ForMember(dto => dto.SubscriptionsCount, o => o.MapFrom(e => e.CompanySubscriptions.Count() + e.JobCategorySubscriptions.Count()))
            .ForMember(dto => dto.ApplicationsCount, o => o.MapFrom(e => e.JobAdApplications.Count()));
    }
}
