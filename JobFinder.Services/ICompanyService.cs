using JobFinder.Web.Models.Company;
using System.Threading.Tasks;

namespace JobFinder.Services
{
    public interface ICompanyService
    {
        Task<int> GetCompanyId(string userId);

        Task<CompanyDetailsUserViewModel> Details(int companyId, string currentUserId);
    }
}
