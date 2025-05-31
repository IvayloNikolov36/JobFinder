using AutoMapper;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Transfer.DTOs.Company;
using JobFinder.Web.Models.CompanyProfile;

namespace JobFinder.Services.Implementations
{
    public class CompanyProfileService : ICompanyProfileService
    {
        public readonly IEntityFrameworkUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CompanyProfileService(IEntityFrameworkUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<CompanyProfileDataViewModel> GetProfileData(string userId)
        {
            CompanyProfileDataDTO companyProfileData = await this.unitOfWork
                .CompanyProfileRepository
                .Get(userId);

            return this.mapper.Map<CompanyProfileDataViewModel>(companyProfileData);
        }
    }
}
