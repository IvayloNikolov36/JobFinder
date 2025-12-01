using AutoMapper;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Transfer.DTOs.Company;
using JobFinder.Web.Models.Company;
using Microsoft.AspNetCore.Http;

namespace JobFinder.Services.Implementations
{
    public class CompaniesService : ICompaniesService
    {
        private readonly IEntityFrameworkUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ICloudImageManagementService cloudImageService;

        public CompaniesService(
            IEntityFrameworkUnitOfWork unitOfWork,
            IMapper mapper,
            ICloudImageManagementService imageManagementService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.cloudImageService = imageManagementService;
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

        public async Task<CompanyJobAdsListingViewModel> AllActiveAds(int companyId)
        {
            CompanyJobAdsListingDTO data = await this.unitOfWork.CompanyRepository
                .AllActiveAds(companyId);

            int? logoImageId = data.CompanyDetails.LogoImageId;
            if (data.CompanyDetails.LogoImageId.HasValue)
            {
                data.CompanyDetails.Logo = await this.cloudImageService.GetThumbnailUrl(logoImageId.Value);
            }
            
            return this.mapper.Map<CompanyJobAdsListingViewModel>(data);
        }

        public async Task<IEnumerable<CompanyListingViewModel>> GetAll(string userId)
        {
            IAsyncEnumerable<CompanyListingDTO> companiesData = this.unitOfWork
                .CompanyRepository
                .GetAll(userId);

            Queue<CompanyListingViewModel> models = new();

            await foreach (CompanyListingDTO companyDto in companiesData)
            {
                CompanyListingViewModel model = this.mapper.Map<CompanyListingViewModel>(companyDto);

                if (companyDto.LogoId.HasValue)
                {
                    model.Logo = await this.cloudImageService
                        .GetThumbnailUrl(companyDto.LogoId.Value);
                }
                
                models.Enqueue(model);
            }

            return models;
        }

        public async Task SetLogo(int id, string userId, IFormFile logo)
        {
            int imageId = (await this.cloudImageService
                .UploadImage(logo, userId))
                .Id;

            await this.unitOfWork.CompanyRepository.SetLogoImageId(id, imageId);

            await this.unitOfWork.SaveChanges();
        }
    }
}
