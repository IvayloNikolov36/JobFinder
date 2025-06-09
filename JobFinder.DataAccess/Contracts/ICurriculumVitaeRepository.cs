using JobFinder.Transfer.DTOs.CV;

namespace JobFinder.DataAccess.Contracts;

public interface ICurriculumVitaeRepository
{
    Task<IEnumerable<CVListingDTO>> All(string userId);

    Task Create(string userId, CVCreateDTO cvData);

    Task<string> GetUserId(string curriculumVitaeId);
   
    Task<T> GetCvData<T>(string cvId) where T: class;

    Task Delete(string cvId);
}
