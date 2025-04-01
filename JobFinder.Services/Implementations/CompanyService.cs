using JobFinder.Common.Exceptions;
using JobFinder.Data.Models;
using JobFinder.DataAccess.Generic;
using JobFinder.Services.Mappings;
using JobFinder.Web.Models.Company;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Services.Implementations
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepository<CompanyEntity> companiesRepository;

        public CompanyService(IRepository<CompanyEntity> companiesRepository)
        {
            this.companiesRepository = companiesRepository;
        }

        public async Task<int> GetCompanyId(string userId)
        {
            int? companyId = await this.companiesRepository
                .Where(c => c.UserId == userId)
                .Select(c => (int?)c.Id)
                .SingleOrDefaultAsync();

            if (!companyId.HasValue)
            {
                throw new ActionableException($"There is no company related to user with id {userId}!");
            }

            return companyId.Value;
        }

        public async Task<CompanyDetailsUserViewModel> Details(int companyId, string currentUserId)
        {
            CompanyDetailsUserViewModel companyDetails = await this.companiesRepository
                .Where(c => c.Id == companyId)
                .To<CompanyDetailsUserViewModel>(new { currentUserId })
                .SingleOrDefaultAsync();

            if (companyDetails == null)
            {
                throw new ActionableException($"Company with id {companyId} does not exist!");
            }

            return companyDetails;
        }
    }
}
