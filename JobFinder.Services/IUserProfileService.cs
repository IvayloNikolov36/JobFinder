using JobFinder.Web.Models.UserProfile;
using System.Threading.Tasks;

namespace JobFinder.Services
{
    public interface IUserProfileService : IService
    {
        Task<UserProfileDataViewModel> GetNyProfileData(string userId);
    }
}
