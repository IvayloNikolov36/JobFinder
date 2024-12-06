using JobFinder.Data.Models;
using JobFinder.Data.Models.Nomenclature;
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
        private readonly IRepository<CountryEntity> countriesRepository;
        private readonly IRepository<CitizenshipEntity> citizenshipRepository;
        private readonly IRepository<GenderEntity> genderRepository;
        private readonly IRepository<BusinessSectorEntity> businessSectorRepository;
        private readonly IRepository<JobCategoryEntity> jobCategoryRepository;
        private readonly IRepository<JobEngagementEntity> jobEngagementRepository;
        private readonly IRepository<EducationLevelEntity> educationLevelsRepository;
        private readonly IRepository<LanguageTypeEntity> languageTypesRepository;
        private readonly IRepository<LanguageLevelEntity> languageLevelsRepository;

        public NomenclatureService(
            IRepository<CountryEntity> countriesRepository,
            IRepository<CitizenshipEntity> citizenshipRepository,
            IRepository<GenderEntity> genderRepository,
            IRepository<BusinessSectorEntity> businessSectorRepository,
            IRepository<JobCategoryEntity> jobCategoryRepository,
            IRepository<JobEngagementEntity> jobEngagementRepository,
            IRepository<EducationLevelEntity> educationLevelsRepository,
            IRepository<LanguageTypeEntity> languageTypesRepository,
            IRepository<LanguageLevelEntity> languageLevelsRepository)
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
