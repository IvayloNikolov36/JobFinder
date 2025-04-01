using JobFinder.Data.Models;
using JobFinder.DataAccess.Generic;
using JobFinder.Services.Mappings;
using JobFinder.Web.Models.CompanyProfile;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Services.Implementations
{
    public class CompanyProfileService : ICompanyProfileService
    {
        public readonly IRepository<CompanyEntity> companyRepository;

        public CompanyProfileService(IRepository<CompanyEntity> companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public async Task<CompanyProfileDataViewModel> GetProfileData(string userId)
        {
            CompanyProfileDataViewModel data =  await this.companyRepository
                .Where(c => c.UserId == userId)
                .To<CompanyProfileDataViewModel>()
                .SingleOrDefaultAsync();

            data.NewApplicationsCount = data.NewApplications.Sum();

            return data;
        }
    }
}
