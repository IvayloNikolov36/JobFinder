using JobFinder.Transfer.DTOs;
using JobFinder.Transfer.DTOs.AnonymousProfile;
using JobFinder.Transfer.DTOs.Cv;
using JobFinder.Transfer.DTOs.JobAd;

namespace JobFinder.DataAccess.Contracts;

public interface IAnonymousProfileRepository
{
    Task Create(string cvId, string userId, AnonymousProfileCreateDTO anonymousProfileDto);

    Task Delete(string id);

    Task<AnonymousProfileDataDTO> GetAnonymousProfile(string anonymousProfileId);

    Task<AnonymousProfileDataDTO> GetAnonymousProfileData(string userId);

    Task<MyAnonymousProfileDataDTO> GetMyAnonymousProfileData(string userId);

    Task<string> GetCvId(string id);

    Task<string> GetOwnerId(string id);

    Task<IEnumerable<AnonymousProfileListingDTO>> GetProfilesRelevantToJobAd(JobAdCriteriasDTO jobAdCriterias);

    Task<bool> IsAnonymousProfileRelevantForJobAd(string id, JobAdCriteriasDTO jobAdCriterias);

    Task<bool> HasAnonymousProfile(string userId);

    Task<IEnumerable<CvPreviewRequestListingDTO>> GetAllCvPreviewRequests(string userId);
}
