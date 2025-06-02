using AutoMapper;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Transfer.DTOs.User;
using JobFinder.Web.Models.UserProfile;

namespace JobFinder.Services.Implementations;

public class UserProfileService : IUserProfileService
{
    private readonly IEntityFrameworkUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public UserProfileService(IEntityFrameworkUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<UserProfileDataViewModel> GetNyProfileData(string userId)
    {
        UserProfileDataDTO userProfile = await this.unitOfWork.UserRepository.GetProfileData(userId);
        
        return this.mapper.Map<UserProfileDataViewModel>(userProfile);
    }
}
