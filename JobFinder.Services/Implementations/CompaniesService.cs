using AutoMapper;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Transfer.DTOs.Company;
using JobFinder.Web.Models.Company;

namespace JobFinder.Services.Implementations
{
    public class CompaniesService : ICompaniesService
    {
        private readonly IEntityFrameworkUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CompaniesService(IEntityFrameworkUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<int> GetCompanyId(string userId)
        {
            return await this.unitOfWork.CompanyRepository.GetCompanyId(userId);
        }

        public async Task<CompanyDetailsUserViewModel> Details(int companyId, string currentUserId)
        {
            CompanyDetailsUserDTO companyDetails = await this.unitOfWork.CompanyRepository
                .GetDetails(companyId, currentUserId);

            return this.mapper.Map<CompanyDetailsUserViewModel>(companyDetails);
        }
    }
}
