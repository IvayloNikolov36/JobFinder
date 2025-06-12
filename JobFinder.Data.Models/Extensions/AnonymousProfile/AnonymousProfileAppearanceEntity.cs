using AutoMapper;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.AnonymousProfile;
using System.Linq;

namespace JobFinder.Data.Models.AnonymousProfile;

public partial class AnonymousProfileAppearanceEntity : IHaveCustomMappings
{
    public void CreateMappings(IProfileExpression configuration)
    {
        configuration.CreateMap<AnonymousProfileAppearanceEntity, AnonymousProfileAppearanceDTO>()
            .ForMember(dto => dto.WorkplaceTypes, o => o.MapFrom(e => e.WorkplaceTypes.Select(wpt => wpt.WorkplaceTypeId)))
            .ForMember(dto => dto.JobEngagements, o => o.MapFrom(e => e.JobEngagements.Select(je => je.JobEngagementId)))
            .ForMember(dto => dto.SoftSkills, o => o.MapFrom(e => e.SoftSkills.Select(ss => ss.SoftSkillId)))
            .ForMember(dto => dto.ITAreas, o => o.MapFrom(e => e.ITAreas.Select(a => a.ITAreaId)))
            .ForMember(dto => dto.TechStacks, o => o.MapFrom(e => e.TechStacks.Select(ts => ts.TechStackId)));
    }
}
