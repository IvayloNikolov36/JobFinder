using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Cv;

namespace JobFinder.Data.Models.Cv;

public partial class PersonalInfoEntity : IMapFrom<PersonalInfoInputDTO>,
    IMapFrom<PersonalInfoEditDTO>,
    IMapTo<PersonalInfoAnonymousProfileDTO>,
    IMapTo<PersonalInfoDTO>
{
}
