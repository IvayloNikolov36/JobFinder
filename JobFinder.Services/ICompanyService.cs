namespace JobFinder.Services
{
    using JobFinder.Data.Models;
    using System.Threading.Tasks;

    public interface ICompanyService
    {
        Task<Company> GetAsync(string userId);
    }
}
