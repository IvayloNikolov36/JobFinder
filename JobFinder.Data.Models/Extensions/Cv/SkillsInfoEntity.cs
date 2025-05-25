using AutoMapper;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.CV;
using System.Linq;

namespace JobFinder.Data.Models.CV;

public partial class SkillsInfoEntity : IHaveCustomMappings
{
    public void CreateMappings(IProfileExpression configuration)
    {
        configuration.CreateMap<SkillsInfoEntity, SkillsInfoDTO>()
            .ForMember(dto => dto.DrivingLicenseCategories, o => o.MapFrom(e => e
                .SkillsInfoDrivingCategories
                .Select(x => x.DrivingCategory)));
    }
}
