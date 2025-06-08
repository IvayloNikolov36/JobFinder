using JobFinder.Services;
using JobFinder.Web.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Web.Controllers
{
    public class NomenclatureController : ApiController
    {
        private readonly INomenclatureService nomenclatureService;

        public NomenclatureController(INomenclatureService nomenclatureService)
        {
            this.nomenclatureService = nomenclatureService;
        }

        [HttpGet]
        [Route("countries")]
        public async Task<IActionResult> GetCountries()
        {
            IEnumerable<BasicViewModel> countries = await this.nomenclatureService.GetCountries();

            return this.Ok(countries);
        }

        [HttpGet]
        [Route("citizenships")]
        public async Task<IActionResult> GetCitizenships()
        {
            IEnumerable<BasicViewModel> citizenships = await this.nomenclatureService.GetCitizenships();

            return this.Ok(citizenships);
        }

        [HttpGet]
        [Route("gender")]
        public async Task<IActionResult> GetGender()
        {
            IEnumerable<BasicViewModel> gender = await this.nomenclatureService.GetGender();

            return this.Ok(gender);
        }

        [HttpGet]
        [Route("business-sectors")]
        public async Task<IActionResult> GetBusinessSectors()
        {
            IEnumerable<BasicViewModel> data = await this.nomenclatureService.GetBusinessSector();

            return this.Ok(data);
        }

        [HttpGet]
        [Route("job-categories")]
        public async Task<IActionResult> GetJobCategories()
        {
            IEnumerable<BasicViewModel> data = await this.nomenclatureService.GetJobCategories();

            return this.Ok(data);
        }

        [HttpGet]
        [Route("job-engagements")]
        public async Task<IActionResult> GetJobEngagements()
        {
            IEnumerable<BasicViewModel> data = await this.nomenclatureService.GetJobEngagements();

            return this.Ok(data);
        }

        [HttpGet]
        [Route("education-levels")]
        public async Task<IActionResult> GetEducationLevels()
        {
            IEnumerable<BasicViewModel> data = await this.nomenclatureService.GetEducationLevels();

            return this.Ok(data);
        }

        [HttpGet]
        [Route("language-types")]
        public async Task<IActionResult> GetLanguageTypes()
        {
            IEnumerable<BasicViewModel> data = await this.nomenclatureService.GetLanguageTypes();

            return this.Ok(data);
        }

        [HttpGet]
        [Route("language-levels")]
        public async Task<IActionResult> GetLanguageLevels()
        {
            IEnumerable<BasicViewModel> data = await this.nomenclatureService.GetLanguageLevels();

            return this.Ok(data);
        }

        [HttpGet]
        [Route("driving-categories")]
        public async Task<IActionResult> GetDrivingCategories()
        {
            IEnumerable<BasicViewModel> data = await this.nomenclatureService.GetDrivingCategories();

            return this.Ok(data);
        }

        [HttpGet]
        [Route("cities")]
        public async Task<IActionResult> GetCities()
        {
            IEnumerable<BasicViewModel> cities = await this.nomenclatureService.GetCities();

            return this.Ok(cities);
        }

        [HttpGet]
        [Route("currencies")]
        public async Task<IActionResult> GetCurrencies()
        {
            IEnumerable<BasicViewModel> currencies = await this.nomenclatureService.GetCurrencies();

            return this.Ok(currencies);
        }

        [HttpGet]
        [Route("recurring-types")]
        public async Task<IActionResult> GetRecurringTypes()
        {
            IEnumerable<BasicViewModel> recurringTypes = await this.nomenclatureService.GetRecurringTypes();

            return this.Ok(recurringTypes);
        }

        [HttpGet]
        [Route("it-areas")]
        public async Task<IActionResult> GetItAreas()
        {
            IEnumerable<BasicViewModel> itAreas = await this.nomenclatureService.GetItAreas();

            return this.Ok(itAreas);
        }

        [HttpGet]
        [Route("tech-stacks")]
        public async Task<IActionResult> GetTechStacks()
        {
            IEnumerable<BasicViewModel> techStacks = await this.nomenclatureService.GetTechStacks();

            return this.Ok(techStacks);
        }

        [HttpGet]
        [Route("soft-skills")]
        public async Task<IActionResult> GetSoftSkills()
        {
            IEnumerable<BasicViewModel> softSkills = await this.nomenclatureService.GetSoftSkills();

            return this.Ok(softSkills);
        }

        [HttpGet]
        [Route("remote-job-preferences")]
        public async Task<IActionResult> GetRemoteJobPreferences()
        {
            IEnumerable<BasicViewModel> remoteJobPreferences = await this.nomenclatureService.GetRemoteJobPreferences();

            return this.Ok(remoteJobPreferences);
        }

        [HttpGet]
        [Route("workplace-types")]
        public async Task<IActionResult> GetWorkplaceTypes()
        {
            IEnumerable<BasicViewModel> workplaceTypes = await this.nomenclatureService.GetWorkplaceTypes();

            return this.Ok(workplaceTypes);
        }
    }
}
