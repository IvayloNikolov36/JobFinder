using JobFinder.Transfer.DTOs;
using JobFinder.Transfer.DTOs.AnonymousProfile;

namespace JobFinder.DataAccess.Contracts;

public interface IAnonymousProfileRepository
{
    Task Create(string cvId, string userId, AnonymousProfileCreateDTO anonymousProfileDto);

    Task Delete(string id);

    Task<AnonymousProfileDataDTO> GetAnonymousProfileData(string userId);

    Task<string> GetCvId(string id);

    Task<string> GetOwnerId(string id);

    Task<bool> HasAnonymousProfile(string userId);
}
