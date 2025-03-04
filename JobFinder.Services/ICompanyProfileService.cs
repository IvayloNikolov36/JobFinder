using JobFinder.Web.Models.CompanyProfile;
using System.Threading.Tasks;

namespace JobFinder.Services
{
    public interface ICompanyProfileService
    {
        Task<CompanyProfileDataViewModel> GetProfileData(string userId);
    }
}
