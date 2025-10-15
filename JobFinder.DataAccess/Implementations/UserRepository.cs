using JobFinder.Data;
using JobFinder.Data.Models;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.User;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.DataAccess.Implementations;

public class UserRepository : EfCoreRepository<UserEntity>, IUserRepository
{
    public UserRepository(JobFinderDbContext context) : base(context)
    {
    }

    public async Task<UserProfileDataDTO> GetProfileData(string userId)
    {
        UserProfileDataDTO userProfile = await this.DbSet.AsNoTracking()
            .Where(u => u.Id == userId)
            .To<UserProfileDataDTO>()
            .SingleOrDefaultAsync();

        base.ValidateForExistence(userProfile, nameof(UserEntity));

        return userProfile;
    }

    public async Task SetProfilePicture(string userId, int pictureId)
    {
        UserEntity user = await this.DbSet.FindAsync(userId);

        base.ValidateForExistence(user, nameof(UserEntity));

        user.ProfilePictureId = pictureId;

        this.DbSet.Update(user);
    }
}
