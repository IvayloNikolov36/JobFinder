using JobFinder.Transfer.DTOs.Company;

namespace JobFinder.DataAccess.Contracts;

public interface ICompanyRepository
{
    Task<CompanyProfileDataDTO> Get(string userId);

    Task<int> GetCompanyId(string userId);

    Task<CompanyDetailsUserDTO> GetDetails(int companyId, string currentUserId);

    Task<CompanyJobAdsListingDTO> AllActiveAds(int companyId);
}
