using JobFinder.Transfer.DTOs.User;

namespace JobFinder.DataAccess.Contracts;

public interface IUserRepository
{
    Task<UserProfileDataDTO> GetProfileData(string userId);
}
