using JobFinder.Transfer.DTOs.Cv;   

namespace JobFinder.DataAccess.Contracts;

public interface ICvRepository
{
    Task<IEnumerable<CVListingDTO>> All(string userId);

    Task Create(string userId, CVCreateDTO cvData);

    Task<string> GetUserId(string curriculumVitaeId);
   
    Task<T> GetCvData<T>(string cvId) where T: class;

    Task Delete(string cvId);
}
