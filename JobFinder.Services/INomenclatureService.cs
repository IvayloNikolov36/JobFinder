using JobFinder.Web.Models.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobFinder.Services
{
    public interface INomenclatureService
    {
        Task<IEnumerable<BasicViewModel>> GetCountries();

        Task<IEnumerable<BasicViewModel>> GetCitizenships();

        Task<IEnumerable<BasicViewModel>> GetGender();
    }
}
