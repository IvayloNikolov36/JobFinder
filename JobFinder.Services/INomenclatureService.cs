﻿using JobFinder.Web.Models.Common;

namespace JobFinder.Services
{
    public interface INomenclatureService
    {
        Task<IEnumerable<BasicViewModel>> GetCountries();

        Task<IEnumerable<BasicViewModel>> GetCitizenships();

        Task<IEnumerable<BasicViewModel>> GetGender();

        Task<IEnumerable<BasicViewModel>> GetBusinessSector();

        Task<IEnumerable<BasicViewModel>> GetJobCategories();

        Task<IEnumerable<BasicViewModel>> GetJobEngagements();

        Task<IEnumerable<BasicViewModel>> GetEducationLevels();

        Task<IEnumerable<BasicViewModel>> GetLanguageTypes();

        Task<IEnumerable<BasicViewModel>> GetLanguageLevels();

        Task<IEnumerable<BasicViewModel>> GetDrivingCategories();

        Task<IEnumerable<BasicViewModel>> GetCities();

        Task<IEnumerable<BasicViewModel>> GetCurrencies();

        Task<IEnumerable<BasicViewModel>> GetRecurringTypes();

        IEnumerable<BasicViewModel> GetRecurringTypesSync();

        Task<IEnumerable<BasicViewModel>> GetItAreas();

        Task<IEnumerable<BasicViewModel>> GetTechStacks();

        Task<IEnumerable<BasicViewModel>> GetSoftSkills();

        Task<IEnumerable<BasicViewModel>> GetWorkplaceTypes();
    }
}
