using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Cv;
using JobFinder.Web.Models.Common;

namespace JobFinder.Web.Models.CvModels;

public class PersonalInfoAnonymousProfileViewModel : IMapFrom<PersonalInfoAnonymousProfileDTO>
{
    public int Id { get; set; }

    public BasicViewModel Gender { get; set; }

    public BasicViewModel Citizenship { get; set; }

    public string City { get; set; }
}
