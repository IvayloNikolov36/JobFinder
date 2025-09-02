using AutoMapper;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Company;
using JobFinder.Transfer.DTOs.JobAd;
using System.Linq;

namespace JobFinder.Data.Models;

public partial class JobAdEntity :
    IMapFrom<JobAdCreateDTO>,
    IMapFrom<JobAdEditDTO>,
    IHaveCustomMappings
{
    public void CreateMappings(IProfileExpression configuration)
    {
        configuration.CreateMap<JobAdEntity, JobAdListingConciseDTO>()
            .ForMember(dto => dto.PostedOn, m => m.MapFrom(e => e.CreatedOn.ToString()))
            .ForMember(dto => dto.Currency, m => m.MapFrom(e => e.Currency.Name))
            .ForMember(dto => dto.JobEngagement, m => m.MapFrom(e => e.JobEngagement.Name))
            .ForMember(dto => dto.JobCategory, m => m.MapFrom(e => e.JobCategory.Name))
            .ForMember(dto => dto.Location, m => m.MapFrom(e => e.Location.Name));

        configuration.CreateMap<JobAdEntity, JobAdListingDTO>()
            .IncludeBase<JobAdEntity, JobAdListingConciseDTO>()
            .ForMember(dto => dto.CompanyId, m => m.MapFrom(e => e.PublisherId))
            .ForMember(dto => dto.CompanyLogo, m => m.MapFrom(e => e.Publisher.Logo))
            .ForMember(dto => dto.CompanyName, m => m.MapFrom(e => e.Publisher.Name));

        configuration.CreateMap<JobAdEntity, JobAdDetailsForSubscriberDTO>()
            .ForMember(dto => dto.Location, o => o.MapFrom(e => e.Location.Name))
            .ForMember(dto => dto.Company, o => o.MapFrom(e => e.Publisher));

        configuration.CreateMap<JobAdEntity, CompanyJobAdDTO>()
            .ForMember(dto => dto.Location, o => o.MapFrom(e => e.Location.Name))
            .ForMember(dto => dto.ApplicationsSent, o => o.MapFrom(e => e.JobAdApplications.Count()))
            .ForMember(x => x.Currency, m => m.MapFrom(m => m.Currency.Name))
            .ForMember(dto => dto.NotPreviewedApplications, o => o.MapFrom(e => e
                .JobAdApplications
                .Where(ja => !ja.PreviewDate.HasValue)
                .Count()));

        configuration.CreateMap<JobAdEntity, JobAdCriteriasDTO>()
            .ForMember(dto => dto.CityId, o => o.MapFrom(e => e.LocationId))
            .ForMember(dto => dto.SoftSkills, o => o.MapFrom(e => e.JobAdSoftSkills.Select(jass => jass.SoftSkillId)))
            .ForMember(dto => dto.ITAreas, o => o.MapFrom(e => e.JobAdITAreas.Select(jass => jass.ItAreaId)))
            .ForMember(dto => dto.TechStacks, o => o.MapFrom(e => e.JobAdTechStacks.Select(jass => jass.TechStackId)));

        configuration.CreateMap<JobAdEntity, CompanyJobAdDetailsDTO>()
            .ForMember(dto => dto.SoftSkills, o => o.MapFrom(e => e.JobAdSoftSkills.Select(jass => jass.SoftSkillId)))
            .ForMember(dto => dto.ITAreas, o => o.MapFrom(e => e.JobAdITAreas.Select(jass => jass.ItAreaId)))
            .ForMember(dto => dto.TechStacks, o => o.MapFrom(e => e.JobAdTechStacks.Select(jass => jass.TechStackId)));
    }
}
