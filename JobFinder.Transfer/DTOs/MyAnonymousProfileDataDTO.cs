using JobFinder.Transfer.DTOs.AnonymousProfile;

namespace JobFinder.Transfer.DTOs;

public class MyAnonymousProfileDataDTO : AnonymousProfileDataDTO
{
    public AnonymousProfileAppearanceDTO ProfileAppearanceCriterias { get; set; }
}
