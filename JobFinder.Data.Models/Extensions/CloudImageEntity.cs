using AutoMapper;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs;

namespace JobFinder.Data.Models;

public partial class CloudImageEntity : IHaveCustomMappings
{
    public void CreateMappings(IProfileExpression configuration)
    {
        configuration.CreateMap<CloudImageDTO, CloudImageEntity>()
            .ForMember(e => e.Id, o =>
            {
                o.UseDestinationValue();
                o.Ignore();
            });
    }
}
