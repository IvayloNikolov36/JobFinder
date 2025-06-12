using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs;

namespace JobFinder.Web.Models.AnonymousProfile;

public class MyAnonymousProfileDataViewModel : AnonymousProfileDataViewModel, IMapFrom<MyAnonymousProfileDataDTO>
{
    public AnonymousProfileAppearanceCriteriaViewModel ProfileAppearanceCriterias { get; set; }
}
