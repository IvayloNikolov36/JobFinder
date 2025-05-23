using JobFinder.Transfer.DTOs;

namespace JobFinder.DataAccess.Contracts;

public interface ICurriculumVitaeRepository
{
    Task<string> GetUserId(string curriculumVitaeId);

    Task<AnonymousProfileCvDataDTO> GetAnonymousProfileCvData(string userId);
}
