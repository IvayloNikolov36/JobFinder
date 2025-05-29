using JobFinder.Transfer.DTOs.CV;

namespace JobFinder.DataAccess.Contracts;

public interface IEducationInfoRepository
{
    Task<IEnumerable<EducationInfoEditDTO>> Update(string cvId, IEnumerable<EducationInfoEditDTO> educationDtos);

    Task SetIncludeInAnonymousProfile(string cvId, IEnumerable<int> educationInfoIds);

    Task DisassociateFromAnonymousProfile(string cvId);
}
