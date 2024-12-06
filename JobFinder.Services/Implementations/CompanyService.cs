namespace JobFinder.Services.Implementations
{
    using JobFinder.Data.Models;
    using JobFinder.Data.Repositories.Contracts;
    using System.Threading.Tasks;

    public class CompanyService : ICompanyService
    {
        private readonly IRepository<CompanyEntity> companyRepository;

        public CompanyService(IRepository<CompanyEntity> companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public async Task<CompanyEntity> GetAsync(string userId)
        {
            return await this.companyRepository
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}
