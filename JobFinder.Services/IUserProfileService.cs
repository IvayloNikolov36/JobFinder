using JobFinder.Web.Models.CloudImage;
using JobFinder.Web.Models.UserProfile;
using Microsoft.AspNetCore.Http;

namespace JobFinder.Services
{
    public interface IUserProfileService : IService
    {
        Task<UserProfileDataViewModel> GetMyProfile(string userId);

        Task<CloudImageViewModel> ChangeProfilePicture(string userId, IFormFile file);
    }
}
