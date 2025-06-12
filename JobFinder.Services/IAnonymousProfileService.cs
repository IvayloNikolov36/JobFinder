using JobFinder.Web.Models.AnonymousProfile;

namespace JobFinder.Services;

public interface IAnonymousProfileService
{
    Task<string> Create(string cvId, string userId, AnonymousProfileCreateViewModel profile);

    Task Delete(string cvId);

    Task<AnonymousProfileDataViewModel> GetAnonymousProfile(string anonymousProfileId);

    Task<MyAnonymousProfileDataViewModel> GetMyAnonymousProfileData(string userId);

    Task<string> GetOwnerId(string id);
}
