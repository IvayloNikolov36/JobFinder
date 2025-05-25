using JobFinder.Web.Models.AnonymousProfile;

namespace JobFinder.Services;

public interface IAnonymousProfileService
{
    Task Activate(string userId, AnonymousProfileCreateViewModel profile);

    Task<AnonymousProfileCvDataViewModel> GetAnonymousProfileData(string userId);
}
