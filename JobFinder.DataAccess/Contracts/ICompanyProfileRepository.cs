using JobFinder.Transfer.DTOs.Company;

namespace JobFinder.DataAccess.Contracts;

public interface ICompanyProfileRepository
{
    Task<CompanyProfileDataDTO> Get(string userId);
}
