using JobFinder.Transfer.DTOs;
using JobFinder.Transfer.DTOs.CV;

namespace JobFinder.DataAccess.Contracts;

public interface ICurriculumVitaeRepository
{
    Task<IEnumerable<CVListingDTO>> All(string userId);

    Task Create(string userId, CVCreateDTO cvData);

    Task<string> GetUserId(string curriculumVitaeId);

    Task<AnonymousProfileCvDataDTO> GetAnonymousProfileCvData(string userId);

    Task SetAnonymousProfileActivated(string cvId);

    Task DeactivateAnonymousProfile(string cvId);

    Task<bool> HasAnyCvWithActivatedAnonymousProfile(string userId);

    Task<bool> HasActivatedAnonymousProfile(string cvId);

    Task<T> GetCvData<T>(string cvId) where T: class;

    Task<byte[]> GetCvData(string cvId);

    Task SetData(string cvId, byte[] data);

    Task Delete(string cvId);
}
