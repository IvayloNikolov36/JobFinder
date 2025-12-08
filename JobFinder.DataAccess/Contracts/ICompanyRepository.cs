using JobFinder.Transfer.DTOs.Company;

namespace JobFinder.DataAccess.Contracts;

public interface ICompanyRepository
{
    Task<CompanyProfileDataDTO> Get(string userId);

    Task<int> GetCompanyId(string userId);

    Task<string> GetUserId(int companyId);

    Task<CompanyDetailsUserDTO> GetDetails(int companyId, string currentUserId);

    Task<CompanyJobAdsListingDTO> AllActiveAds(int companyId);

    IAsyncEnumerable<CompanyListingDTO> GetAll(string userId);

    Task SetLogoImageId(int companyId, int imageId);

    Task Update(int id, CompanyEditDTO companyDto);
}
