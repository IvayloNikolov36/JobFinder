using AutoMapper;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Transfer.DTOs.Company;
using JobFinder.Web.Models.CloudImage;
using JobFinder.Web.Models.CompanyProfile;
using Microsoft.AspNetCore.Http;

namespace JobFinder.Services.Implementations
{
    public class CompanyProfileService : ICompanyProfileService
    {
        public readonly IEntityFrameworkUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ICloudImageManagementService imageService;

        public CompanyProfileService(
            IEntityFrameworkUnitOfWork unitOfWork,
            IMapper mapper,
            ICloudImageManagementService imageService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.imageService = imageService;
        }

        public async Task<CompanyProfileDataViewModel> GetProfileData(string userId)
        {
            CompanyProfileDataDTO companyProfileData = await this.unitOfWork
                .CompanyRepository
                .Get(userId);

            var model = this.mapper.Map<CompanyProfileDataViewModel>(companyProfileData);

            if (companyProfileData.LogoImageId.HasValue)
            {
                model.LogoThumbnailUrl = await this.imageService
                    .GetThumbnailUrl(companyProfileData.LogoImageId.Value);
            }

            return model;
        }

        public async Task<CloudImageViewModel> ChangeLogo(string userId, IFormFile file)
        {
            CloudImageViewModel imageUploadResult = await this.imageService
                .UploadImage(file, userId, replaceCurrent: true);

            int companyId = await this.unitOfWork.CompanyRepository
                .GetCompanyId(userId);

            await this.unitOfWork.CompanyRepository
                .SetLogoImageId(companyId, imageUploadResult.Id);

            await this.unitOfWork.SaveChanges();

            imageUploadResult.Url = this.imageService.GetUrl(imageUploadResult.Url);
            imageUploadResult.ThumbnailUrl = this.imageService.GetUrl(imageUploadResult.ThumbnailUrl);

            return imageUploadResult;
        }
    }
}
