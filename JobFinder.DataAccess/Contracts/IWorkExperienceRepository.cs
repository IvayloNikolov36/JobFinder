using JobFinder.Transfer.DTOs.CV;

namespace JobFinder.DataAccess.Contracts;

public interface IWorkExperienceRepository
{
    Task<IEnumerable<WorkExperienceInfoEditDTO>> Update(
        string cvId,
        IEnumerable<WorkExperienceInfoEditDTO> workExperienceModels);

    Task SetIncludeInAnonymousProfile(string cvId, IEnumerable<int> workExperienceId);

    Task DisassociateFromAnonymousProfile(string cvId);
}
