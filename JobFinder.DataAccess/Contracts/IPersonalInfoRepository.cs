using JobFinder.Transfer.DTOs.Cv;

namespace JobFinder.DataAccess.Contracts;

public interface IPersonalInfoRepository
{
    Task Update(PersonalInfoEditDTO personalInfoDto);

    void Delete(string cvId);
}
