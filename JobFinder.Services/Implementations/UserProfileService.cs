using AutoMapper;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Transfer.DTOs.User;
using JobFinder.Web.Models.CloudImage;
using JobFinder.Web.Models.UserProfile;
using Microsoft.AspNetCore.Http;

namespace JobFinder.Services.Implementations;

public class UserProfileService : IUserProfileService
{
    private readonly IEntityFrameworkUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ICloudImageManagementService imageManagementService;

    public UserProfileService(
        IEntityFrameworkUnitOfWork unitOfWork,
        IMapper mapper,
        ICloudImageManagementService imageManagementService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.imageManagementService = imageManagementService;
    }

    public async Task<UserProfileDataViewModel> GetMyProfile(string userId)
    {
        UserProfileDataDTO profile = await this.unitOfWork.UserRepository
            .GetProfileData(userId);

        var model = this.mapper.Map<UserProfileDataViewModel>(profile);

        model.PictureUrl = await this.imageManagementService
            .GetThumbnailUrl(profile.ProfilePictureId);

        return model;
    }

    public async Task<CloudImageViewModel> ChangeProfilePicture(string userId, IFormFile file)
    {
        CloudImageViewModel imageData = await this.imageManagementService
            .UploadImage(file, userId);

        await this.unitOfWork.UserRepository.SetProfilePicture(userId, imageData.Id);

        await this.unitOfWork.SaveChanges();

        return imageData;
    }
}
