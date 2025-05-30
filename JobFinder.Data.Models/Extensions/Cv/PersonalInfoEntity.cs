using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.CV;

namespace JobFinder.Data.Models.CV;

public partial class PersonalInfoEntity : IMapFrom<PersonalInfoInputDTO>,
    IMapTo<PersonalInfoAnonymousProfileDTO>,
    IMapTo<PersonalInfoDTO>
{

}
