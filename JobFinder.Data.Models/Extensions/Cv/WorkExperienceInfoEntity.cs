using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.CV;

namespace JobFinder.Data.Models.CV;

public partial class WorkExperienceInfoEntity : IMapFrom<WorkExperienceInfoEditDTO>,
    IMapTo<WorkExperienceInfoDTO>,
    IMapTo<WorkExperienceInfoEditDTO>
{

}
