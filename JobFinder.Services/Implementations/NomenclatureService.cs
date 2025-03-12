using JobFinder.Data.Models.Nomenclature;
using JobFinder.Data.Repositories.Contracts;
using JobFinder.Services.Mappings;
using JobFinder.Web.Models.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobFinder.Services.Implementations
{
    public class NomenclatureService : INomenclatureService
    {
        private readonly IRepository<CountryEntity> countriesRepository;
        private readonly IRepository<CitizenshipEntity> citizenshipRepository;
        private readonly IRepository<GenderEntity> genderRepository;
        private readonly IRepository<BusinessSectorEntity> businessSectorRepository;
        private readonly IRepository<JobCategoryEntity> jobCategoryRepository;
        private readonly IRepository<JobEngagementEntity> jobEngagementRepository;
        private readonly IRepository<EducationLevelEntity> educationLevelsRepository;
        private readonly IRepository<LanguageTypeEntity> languageTypesRepository;
        private readonly IRepository<LanguageLevelEntity> languageLevelsRepository;
        private readonly IRepository<DrivingCategoryEntity> drivingCategoryTypesRepository;
        private readonly IRepository<CityEntity> citiesRepository;
        private readonly IRepository<CurrencyEntity> currenciesRepository;
        private readonly IRepository<ReccuringTypeEntity> reccuringTypesRepository;

        public NomenclatureService(
            IRepository<CountryEntity> countriesRepository,
            IRepository<CitizenshipEntity> citizenshipRepository,
            IRepository<GenderEntity> genderRepository,
            IRepository<BusinessSectorEntity> businessSectorRepository,
            IRepository<JobCategoryEntity> jobCategoryRepository,
            IRepository<JobEngagementEntity> jobEngagementRepository,
            IRepository<EducationLevelEntity> educationLevelsRepository,
            IRepository<LanguageTypeEntity> languageTypesRepository,
            IRepository<LanguageLevelEntity> languageLevelsRepository,
            IRepository<DrivingCategoryEntity> drivingCategoryTypesRepository,
            IRepository<CityEntity> citiesRepository,
            IRepository<CurrencyEntity> currenciesRepository,
            IRepository<ReccuringTypeEntity> reccuringTypesRepository)
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
            this.drivingCategoryTypesRepository = drivingCategoryTypesRepository;
            this.citiesRepository = citiesRepository;
            this.currenciesRepository = currenciesRepository;
            this.reccuringTypesRepository = reccuringTypesRepository;
            this.reccuringTypesRepository = reccuringTypesRepository;
        }

        public async Task<IEnumerable<BasicViewModel>> GetCountries()
        {
            return await this.countriesRepository
                .DbSetNoTracking()
                .To<BasicViewModel>()
                .ToArrayAsync();
        }

        public async Task<IEnumerable<BasicViewModel>> GetCitizenships()
        {
            return await this.citizenshipRepository
                .DbSetNoTracking()
                .To<BasicViewModel>()
                .ToArrayAsync();
        }

        public async Task<IEnumerable<BasicViewModel>> GetGender()
        {
            return await this.genderRepository
                .DbSetNoTracking()
                .To<BasicViewModel>()
                .ToArrayAsync();
        }

        public async Task<IEnumerable<BasicViewModel>> GetBusinessSector()
        {
            return await this.businessSectorRepository
                .DbSetNoTracking()
                .To<BasicViewModel>()
                .ToArrayAsync();
        }

        public async Task<IEnumerable<BasicViewModel>> GetJobCategories()
        {
            return await this.jobCategoryRepository
                .DbSetNoTracking()
                .To<BasicViewModel>()
                .ToArrayAsync();
        }

        public async Task<IEnumerable<BasicViewModel>> GetJobEngagements()
        {
            return await this.jobEngagementRepository
                .DbSetNoTracking()
                .To<BasicViewModel>()
                .ToArrayAsync();
        }

        public async Task<IEnumerable<BasicViewModel>> GetEducationLevels()
        {
            return await this.educationLevelsRepository
                .DbSetNoTracking()
                .To<BasicViewModel>()
                .ToArrayAsync();
        }

        public async Task<IEnumerable<BasicViewModel>> GetLanguageTypes()
        {
            return await this.languageTypesRepository
                .DbSetNoTracking()
                .To<BasicViewModel>()
                .ToArrayAsync();
        }

        public async Task<IEnumerable<BasicViewModel>> GetLanguageLevels()
        {
            return await this.languageLevelsRepository
                .DbSetNoTracking()
                .To<BasicViewModel>()
                .ToArrayAsync();
        }

        public async Task<IEnumerable<BasicViewModel>> GetDrivingCategories()
        {
            return await this.drivingCategoryTypesRepository
                .DbSetNoTracking()
                .To<BasicViewModel>()
                .ToArrayAsync();
        }

        public async Task<IEnumerable<BasicViewModel>> GetCities()
        {
            return await this.citiesRepository.DbSetNoTracking()
                .To<BasicViewModel>()
                .ToArrayAsync();
        }

        public async Task<IEnumerable<BasicViewModel>> GetCurrencies()
        {
            return await this.currenciesRepository.DbSetNoTracking()
                .To<BasicViewModel>()
                .ToArrayAsync();
        }

        public async Task<IEnumerable<BasicViewModel>> GetReccuringTypes()
        {
            return await this.reccuringTypesRepository.DbSetNoTracking()
                .To<BasicViewModel>()
                .ToArrayAsync();
        }
    }
}
