using JobFinder.Data.Models;
using JobFinder.DataAccess.Generic;
using JobFinder.Services.Mappings;
using JobFinder.Web.Models.UserProfile;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace JobFinder.Services.Implementations
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IRepository<UserEntity> usersRepository;

        public UserProfileService(IRepository<UserEntity> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task<UserProfileDataViewModel> GetNyProfileData(string userId)
        {
            return await this.usersRepository
                .Where(u => u.Id == userId)
                .To<UserProfileDataViewModel>()
                .SingleOrDefaultAsync();
        }
    }
}
