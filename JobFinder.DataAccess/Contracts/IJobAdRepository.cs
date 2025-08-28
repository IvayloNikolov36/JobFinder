
using JobFinder.Transfer.DTOs;
using JobFinder.Transfer.DTOs.Company;
using JobFinder.Transfer.DTOs.JobAd;

namespace JobFinder.DataAccess.Contracts;

public interface IJobAdRepository
{
    Task<CompanyJobAdDetailsDTO> Get(int id);

    Task<DataListingDTO<JobAdListingDTO>> AllActive(JobAdFilterDTO filter);

    Task Create(JobAdCreateDTO jobAd, int companyId);

    Task Update(int id, JobAdEditDTO jobAdDto);

    Task ExecuteJobAdsDeactivate(DateTime publishDateTreshold);

    Task<string> GetPublisher(int jobAdId);

    Task<JobAdDetailsForSubscriberDTO> GetDetailsForSubscriber(int jobAdId);

    Task<IEnumerable<CompanyJobAdDTO>> GetFilteredCompanyAds(string userId, int? lifecycleStatus);

    Task<JobAdCriteriasDTO> GetJobAdCriterias(int jobAdId);
}
