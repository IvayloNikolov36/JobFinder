namespace JobFinder.Services.Implementations
{
    using JobFinder.Data.Models;
    using JobFinder.Data.Repositories.Contracts;
    using System.Threading.Tasks;

    public class CompanyService : ICompanyService
    {
        private readonly IRepository<Company> companyRepository;

        public CompanyService(IRepository<Company> companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public async Task<Company> GetAsync(string userId)
        {
            return await this.companyRepository
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}
