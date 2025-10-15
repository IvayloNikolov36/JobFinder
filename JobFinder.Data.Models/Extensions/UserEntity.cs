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
            // TODO: set the phoneNumber in userEntity table and get it from there
            .ForMember(dto => dto.Phone, o => o.MapFrom(e => e.Cvs
                .Select(c => c.PersonalInfo.Phone)
                .FirstOrDefault()))
            .ForMember(dto => dto.CVsCount, o => o.MapFrom(e => e.Cvs.Count()))
            .ForMember(dto => dto.SubscriptionsCount, o => o.MapFrom(e => e.CompanySubscriptions.Count() + e.JobCategorySubscriptions.Count()))
            .ForMember(dto => dto.ApplicationsCount, o => o.MapFrom(e => e.JobAdApplications.Count()));
    }
}
