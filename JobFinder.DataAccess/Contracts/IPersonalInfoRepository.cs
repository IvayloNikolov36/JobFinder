using JobFinder.Transfer.DTOs.CV;

namespace JobFinder.DataAccess.Contracts;

public interface IPersonalInfoRepository
{
    Task Update(PersonalInfoEditDTO personalInfoDto);

    void Delete(string cvId);
}
