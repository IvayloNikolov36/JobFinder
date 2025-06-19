using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.User;

namespace JobFinder.Web.Models.UserProfile
{
    public class UserProfileDataViewModel : IMapFrom<UserProfileDataDTO>
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string PictureUrl { get; set; }

        public int CVsCount { get; set; }

        public int SubscriptionsCount { get; set; }

        public int ApplicationsCount { get; set; }
    }
}
