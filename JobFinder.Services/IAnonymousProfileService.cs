using JobFinder.Web.Models.AnonymousProfile;

namespace JobFinder.Services;

public interface IAnonymousProfileService
{
    Task Activate(string cvId, string userId, AnonymousProfileCreateViewModel profile);

    Task Deactivate(string cvId);

    Task<AnonymousProfileCvDataViewModel> GetAnonymousProfileData(string userId);
}
