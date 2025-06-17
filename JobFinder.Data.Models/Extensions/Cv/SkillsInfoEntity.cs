using AutoMapper;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Cv;
using System.Linq;

namespace JobFinder.Data.Models.Cv;

public partial class SkillsInfoEntity : IMapFrom<SkillsInfoInputDTO>,
    IMapFrom<SkillsInfoEditDTO>,
    IHaveCustomMappings
{
    public void CreateMappings(IProfileExpression configuration)
    {
        configuration.CreateMap<SkillsInfoEntity, SkillsInfoDTO>()
            .ForMember(dto => dto.DrivingLicenseCategories, o => o.MapFrom(e => e
                .SkillsInfoDrivingCategories
                .Select(x => x.DrivingCategory)));
    }
}
