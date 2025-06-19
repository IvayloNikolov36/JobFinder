using AutoMapper;
using JobFinder.Data.Models.Nomenclature;
using JobFinder.DataAccess.Generic;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs;
using JobFinder.Web.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Services.Implementations
{
    public class NomenclatureService : INomenclatureService
    {
        private readonly IMapper mapper;
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
        private readonly IRepository<RecurringTypeEntity> recurringTypesRepository;
        private readonly IRepository<ItAreaEntity> itAreasRepository;
        private readonly IRepository<TechStackEntity> techStacksRepository;
        private readonly IRepository<SoftSKillEntity> softSkillsRepository;
        private readonly IRepository<WorkplaceTypeEntity> workplaceTypeRepository;

        public NomenclatureService(
            IMapper mapper,
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
            IRepository<RecurringTypeEntity> recurringTypesRepository,
            IRepository<ItAreaEntity> itAreasRepository,
            IRepository<TechStackEntity> techStacksRepository,
            IRepository<SoftSKillEntity> softSkillsRepository,
            IRepository<WorkplaceTypeEntity> workplaceTypeRepository)
        {
            this.mapper = mapper;
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
            this.recurringTypesRepository = recurringTypesRepository;
            this.recurringTypesRepository = recurringTypesRepository;
            this.itAreasRepository = itAreasRepository;
            this.techStacksRepository = techStacksRepository;
            this.softSkillsRepository = softSkillsRepository;
            this.workplaceTypeRepository = workplaceTypeRepository;
        }

        public async Task<IEnumerable<BasicViewModel>> GetCountries()
        {
            BasicDTO[] data = await this.countriesRepository
                .DbSetNoTracking()
                .To<BasicDTO>()
                .ToArrayAsync();

            return this.mapper.Map<IEnumerable<BasicViewModel>>(data);
        }

        public async Task<IEnumerable<BasicViewModel>> GetCitizenships()
        {
            BasicDTO[] data = await this.citizenshipRepository
                .DbSetNoTracking()
                .To<BasicDTO>()
                .ToArrayAsync();

            return this.mapper.Map<IEnumerable<BasicViewModel>>(data);
        }

        public async Task<IEnumerable<BasicViewModel>> GetGender()
        {
            BasicDTO[] data = await this.genderRepository
                .DbSetNoTracking()
                .To<BasicDTO>()
                .ToArrayAsync();

            return this.mapper.Map<IEnumerable<BasicViewModel>>(data);
        }

        public async Task<IEnumerable<BasicViewModel>> GetBusinessSector()
        {
            BasicDTO[] data = await this.businessSectorRepository
                .DbSetNoTracking()
                .To<BasicDTO>()
                .ToArrayAsync();

            return this.mapper.Map<IEnumerable<BasicViewModel>>(data);
        }

        public async Task<IEnumerable<BasicViewModel>> GetJobCategories()
        {
            BasicDTO[] data = await this.jobCategoryRepository
                .DbSetNoTracking()
                .To<BasicDTO>()
                .ToArrayAsync();

            return this.mapper.Map<IEnumerable<BasicViewModel>>(data);
        }

        public async Task<IEnumerable<BasicViewModel>> GetJobEngagements()
        {
            BasicDTO[] data = await this.jobEngagementRepository
                .DbSetNoTracking()
                .To<BasicDTO>()
                .ToArrayAsync();

            return this.mapper.Map<IEnumerable<BasicViewModel>>(data);
        }

        public async Task<IEnumerable<BasicViewModel>> GetEducationLevels()
        {
            BasicDTO[] data = await this.educationLevelsRepository
                .DbSetNoTracking()
                .To<BasicDTO>()
                .ToArrayAsync();

            return this.mapper.Map<IEnumerable<BasicViewModel>>(data);
        }

        public async Task<IEnumerable<BasicViewModel>> GetLanguageTypes()
        {
            BasicDTO[] data = await this.languageTypesRepository
                .DbSetNoTracking()
                .To<BasicDTO>()
                .ToArrayAsync();

            return this.mapper.Map<IEnumerable<BasicViewModel>>(data);
        }

        public async Task<IEnumerable<BasicViewModel>> GetLanguageLevels()
        {
            BasicDTO[] data = await this.languageLevelsRepository
                .DbSetNoTracking()
                .To<BasicDTO>()
                .ToArrayAsync();

            return this.mapper.Map<IEnumerable<BasicViewModel>>(data);
        }

        public async Task<IEnumerable<BasicViewModel>> GetDrivingCategories()
        {
            BasicDTO[] data = await this.drivingCategoryTypesRepository
                .DbSetNoTracking()
                .To<BasicDTO>()
                .ToArrayAsync();

            return this.mapper.Map<IEnumerable<BasicViewModel>>(data);
        }

        public async Task<IEnumerable<BasicViewModel>> GetCities()
        {
            BasicDTO[] data = await this.citiesRepository.DbSetNoTracking()
                .To<BasicDTO>()
                .ToArrayAsync();

            return this.mapper.Map<IEnumerable<BasicViewModel>>(data);
        }

        public async Task<IEnumerable<BasicViewModel>> GetCurrencies()
        {
            BasicDTO[] data = await this.currenciesRepository.DbSetNoTracking()
                .To<BasicDTO>()
                .ToArrayAsync();

            return this.mapper.Map<IEnumerable<BasicViewModel>>(data);
        }

        public async Task<IEnumerable<BasicViewModel>> GetRecurringTypes()
        {
            BasicDTO[] data = await this.recurringTypesRepository.DbSetNoTracking()
                .To<BasicDTO>()
                .ToArrayAsync();

            return this.mapper.Map<IEnumerable<BasicViewModel>>(data);
        }

        public IEnumerable<BasicViewModel> GetRecurringTypesSync()
        {
            BasicDTO[] data = this.recurringTypesRepository.DbSetNoTracking()
                .To<BasicDTO>()
                .ToArray();

            return this.mapper.Map<IEnumerable<BasicViewModel>>(data);
        }

        public async Task<IEnumerable<BasicViewModel>> GetItAreas()
        {
            BasicDTO[] data = await this.itAreasRepository.DbSetNoTracking()
                .To<BasicDTO>()
                .ToArrayAsync();

            return this.mapper.Map<IEnumerable<BasicViewModel>>(data);
        }

        public async Task<IEnumerable<BasicViewModel>> GetTechStacks()
        {
            BasicDTO[] data = await this.techStacksRepository.DbSetNoTracking()
                .To<BasicDTO>()
                .ToArrayAsync();

            return this.mapper.Map<IEnumerable<BasicViewModel>>(data);
        }

        public async Task<IEnumerable<BasicViewModel>> GetSoftSkills()
        {
            BasicDTO[] data = await this.softSkillsRepository.DbSetNoTracking()
                .To<BasicDTO>()
                .ToArrayAsync();

            return this.mapper.Map<IEnumerable<BasicViewModel>>(data);
        }

        public async Task<IEnumerable<BasicViewModel>> GetWorkplaceTypes()
        {
            BasicDTO[] data = await this.workplaceTypeRepository.DbSetNoTracking()
                .To<BasicDTO>()
                .ToArrayAsync();

            return this.mapper.Map<IEnumerable<BasicViewModel>>(data);
        }
    }
}
