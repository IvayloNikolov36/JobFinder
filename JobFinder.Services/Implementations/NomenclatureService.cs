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
        private readonly IRepository<BusinessSector> businessSectorRepository;
        private readonly IRepository<JobCategory> jobCategoryRepository;
        private readonly IRepository<JobEngagement> jobEngagementRepository;
        private readonly IRepository<EducationLevel> educationLevelsRepository;
        private readonly IRepository<LanguageType> languageTypesRepository;
        private readonly IRepository<LanguageLevel> languageLevelsRepository;

        public NomenclatureService(
            IRepository<Country> countriesRepository,
            IRepository<Citizenship> citizenshipRepository,
            IRepository<Gender> genderRepository,
            IRepository<BusinessSector> businessSectorRepository,
            IRepository<JobCategory> jobCategoryRepository,
            IRepository<JobEngagement> jobEngagementRepository,
            IRepository<EducationLevel> educationLevelsRepository,
            IRepository<LanguageType> languageTypesRepository,
            IRepository<LanguageLevel> languageLevelsRepository)
        {
            this.countriesRepository = countriesRepository;
            this.citizenshipRepository = citizenshipRepository;
            this.genderRepository = genderRepository;
            this.businessSectorRepository = businessSectorRepository;
            this.jobCategoryRepository = jobCategoryRepository;
            this.jobEngagementRepository = jobEngagementRepository;
            this.educationLevelsRepository = educationLevelsRepository;
            this.languageTypesRepository = languageTypesRepository;
            this.languageLevelsRepository = languageLevelsRepository;
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

        public async Task<IEnumerable<BasicViewModel>> GetBusinessSector()
        {
            BasicViewModel[] data = await this.businessSectorRepository
                .All()
                .Select(x => new BasicViewModel(x.Id, x.Name))
                .AsNoTracking()
                .ToArrayAsync();

            return data;
        }

        public async Task<IEnumerable<BasicViewModel>> GetJobCategories()
        {
            BasicViewModel[] data = await this.jobCategoryRepository
                .All()
                .Select(x => new BasicViewModel(x.Id, x.Name))
                .AsNoTracking()
                .ToArrayAsync();

            return data;
        }

        public async Task<IEnumerable<BasicViewModel>> GetJobEngagements()
        {
            BasicViewModel[] data = await this.jobEngagementRepository
                .All()
                .Select(x => new BasicViewModel(x.Id, x.Name))
                .AsNoTracking()
                .ToArrayAsync();

            return data;
        }

        public async Task<IEnumerable<BasicViewModel>> GetEducationLevels()
        {
            BasicViewModel[] data = await this.educationLevelsRepository
                .All()
                .Select(x => new BasicViewModel(x.Id, x.Name))
                .AsNoTracking()
                .ToArrayAsync();

            return data;
        }

        public async Task<IEnumerable<BasicViewModel>> GetLanguageTypes()
        {
            BasicViewModel[] data = await this.languageTypesRepository
                .All()
                .Select(x => new BasicViewModel(x.Id, x.Name))
                .AsNoTracking()
                .ToArrayAsync();

            return data;
        }

        public async Task<IEnumerable<BasicViewModel>> GetLanguageLevels()
        {
            BasicViewModel[] data = await this.languageLevelsRepository
                .All()
                .Select(x => new BasicViewModel(x.Id, x.Name))
                .AsNoTracking()
                .ToArrayAsync();

            return data;
        }
    }
}
