using JobFinder.Data.Models;
using JobFinder.Data.Repositories.Contracts;
using JobFinder.Web.Models.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Services.Implementations
{
    public class NomenclatureService : INomenclatureService
    {
        private readonly IRepository<Country> countriesRepository;
        private readonly IRepository<Citizenship> citizenshipRepository;
        private readonly IRepository<Gender> genderRepository;

        public NomenclatureService(
            IRepository<Country> countriesRepository,
            IRepository<Citizenship> citizenshipRepository,
            IRepository<Gender> genderRepository)
        {
            this.countriesRepository = countriesRepository;
            this.citizenshipRepository = citizenshipRepository;
            this.genderRepository = genderRepository;
        }

        public async Task<IEnumerable<BasicViewModel>> GetCountries()
        {
            BasicViewModel[] countries = await this.countriesRepository
                .All()
                .Select(x => new BasicViewModel(x.Id, x.Name))
                .AsNoTracking()
                .ToArrayAsync();

            return countries;
        }

        public async Task<IEnumerable<BasicViewModel>> GetCitizenships()
        {
            BasicViewModel[] countries = await this.citizenshipRepository
                .All()
                .Select(x => new BasicViewModel(x.Id, x.Name))
                .AsNoTracking()
                .ToArrayAsync();

            return countries;
        }

        public async Task<IEnumerable<BasicViewModel>> GetGender()
        {
            BasicViewModel[] gender = await this.genderRepository
                .All()
                .Select(x => new BasicViewModel(x.Id, x.Name))
                .AsNoTracking()
                .ToArrayAsync();

            return gender;
        }
    }
}
