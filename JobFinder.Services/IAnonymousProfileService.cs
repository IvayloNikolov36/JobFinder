using JobFinder.Web.Models.AnonymousProfile;

namespace JobFinder.Services;

public interface IAnonymousProfileService
{
    Task<string> Create(string cvId, string userId, AnonymousProfileCreateViewModel profile);

    Task Delete(string cvId);

    Task<AnonymousProfileDataViewModel> GetAnonymousProfileData(string userId);

    Task<string> GetOwnerId(string id);
}
