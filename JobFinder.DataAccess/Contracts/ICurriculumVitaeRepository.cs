using JobFinder.Transfer.DTOs;
using JobFinder.Transfer.DTOs.CV;

namespace JobFinder.DataAccess.Contracts;

public interface ICurriculumVitaeRepository
{
    Task<string> GetUserId(string curriculumVitaeId);

    Task<AnonymousProfileCvDataDTO> GetAnonymousProfileCvData(string userId);

    Task SetAnonymousProfileActivated(string cvId);

    Task DeactivateAnonymousProfile(string cvId);

    Task<bool> HasAnyCvWithActivatedAnonymousProfile(string userId);

    Task<bool> HasActivatedAnonymousProfile(string cvId);

    Task<MyCvDataDTO> GetMyCvData(string cvId);
}
