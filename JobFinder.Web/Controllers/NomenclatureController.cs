using JobFinder.Services;
using JobFinder.Web.Models.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Web.Controllers
{
    [Authorize]
    public class NomenclatureController : ApiController
    {
        private const int CacheDuration = 3600;

        private readonly INomenclatureService nomenclatureService;

        public NomenclatureController(INomenclatureService nomenclatureService)
        {
            this.nomenclatureService = nomenclatureService;
        }

        [HttpGet]
        [Route("countries")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BasicViewModel>))]
        [ResponseCache(Duration = CacheDuration)]
        public async Task<IActionResult> GetCountries()
        {
            IEnumerable<BasicViewModel> countries = await this.nomenclatureService.GetCountries();

            return this.Ok(countries);
        }

        [HttpGet]
        [Route("citizenships")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BasicViewModel>))]
        [ResponseCache(Duration = CacheDuration)]
        public async Task<IActionResult> GetCitizenships()
        {
            IEnumerable<BasicViewModel> citizenships = await this.nomenclatureService.GetCitizenships();

            return this.Ok(citizenships);
        }

        [HttpGet]
        [Route("gender")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BasicViewModel>))]
        [ResponseCache(Duration = CacheDuration)]
        public async Task<IActionResult> GetGender()
        {
            IEnumerable<BasicViewModel> gender = await this.nomenclatureService.GetGender();

            return this.Ok(gender);
        }

        [HttpGet]
        [Route("business-sectors")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BasicViewModel>))]
        [ResponseCache(Duration = CacheDuration)]
        public async Task<IActionResult> GetBusinessSectors()
        {
            IEnumerable<BasicViewModel> data = await this.nomenclatureService.GetBusinessSector();

            return this.Ok(data);
        }

        [HttpGet]
        [Route("job-categories")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BasicViewModel>))]
        [ResponseCache(Duration = CacheDuration)]
        public async Task<IActionResult> GetJobCategories()
        {
            IEnumerable<BasicViewModel> data = await this.nomenclatureService.GetJobCategories();

            return this.Ok(data);
        }

        [HttpGet]
        [Route("job-engagements")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BasicViewModel>))]
        [ResponseCache(Duration = CacheDuration)]
        public async Task<IActionResult> GetJobEngagements()
        {
            IEnumerable<BasicViewModel> data = await this.nomenclatureService.GetJobEngagements();

            return this.Ok(data);
        }

        [HttpGet]
        [Route("education-levels")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BasicViewModel>))]
        [ResponseCache(Duration = CacheDuration)]
        public async Task<IActionResult> GetEducationLevels()
        {
            IEnumerable<BasicViewModel> data = await this.nomenclatureService.GetEducationLevels();

            return this.Ok(data);
        }

        [HttpGet]
        [Route("language-types")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BasicViewModel>))]
        [ResponseCache(Duration = CacheDuration)]
        public async Task<IActionResult> GetLanguageTypes()
        {
            IEnumerable<BasicViewModel> data = await this.nomenclatureService.GetLanguageTypes();

            return this.Ok(data);
        }

        [HttpGet]
        [Route("language-levels")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BasicViewModel>))]
        [ResponseCache(Duration = CacheDuration)]
        public async Task<IActionResult> GetLanguageLevels()
        {
            IEnumerable<BasicViewModel> data = await this.nomenclatureService.GetLanguageLevels();

            return this.Ok(data);
        }

        [HttpGet]
        [Route("driving-categories")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BasicViewModel>))]
        [ResponseCache(Duration = CacheDuration)]
        public async Task<IActionResult> GetDrivingCategories()
        {
            IEnumerable<BasicViewModel> data = await this.nomenclatureService.GetDrivingCategories();

            return this.Ok(data);
        }

        [HttpGet]
        [Route("cities")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BasicViewModel>))]
        [ResponseCache(Duration = CacheDuration)]
        public async Task<IActionResult> GetCities()
        {
            IEnumerable<BasicViewModel> cities = await this.nomenclatureService.GetCities();

            return this.Ok(cities);
        }

        [HttpGet]
        [Route("currencies")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BasicViewModel>))]
        [ResponseCache(Duration = CacheDuration)]
        public async Task<IActionResult> GetCurrencies()
        {
            IEnumerable<BasicViewModel> currencies = await this.nomenclatureService.GetCurrencies();

            return this.Ok(currencies);
        }

        [HttpGet]
        [Route("recurring-types")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BasicViewModel>))]
        [ResponseCache(Duration = CacheDuration)]
        public async Task<IActionResult> GetRecurringTypes()
        {
            IEnumerable<BasicViewModel> recurringTypes = await this.nomenclatureService.GetRecurringTypes();

            return this.Ok(recurringTypes);
        }

        [HttpGet]
        [Route("it-areas")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BasicViewModel>))]
        [ResponseCache(Duration = CacheDuration)]
        public async Task<IActionResult> GetItAreas()
        {
            IEnumerable<BasicViewModel> itAreas = await this.nomenclatureService.GetItAreas();

            return this.Ok(itAreas);
        }

        [HttpGet]
        [Route("tech-stacks")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BasicViewModel>))]
        [ResponseCache(Duration = CacheDuration)]
        public async Task<IActionResult> GetTechStacks()
        {
            IEnumerable<BasicViewModel> techStacks = await this.nomenclatureService.GetTechStacks();

            return this.Ok(techStacks);
        }

        [HttpGet]
        [Route("soft-skills")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BasicViewModel>))]
        [ResponseCache(Duration = CacheDuration)]
        public async Task<IActionResult> GetSoftSkills()
        {
            IEnumerable<BasicViewModel> softSkills = await this.nomenclatureService.GetSoftSkills();

            return this.Ok(softSkills);
        }

        [HttpGet]
        [Route("workplace-types")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BasicViewModel>))]
        [ResponseCache(Duration = CacheDuration)]
        public async Task<IActionResult> GetWorkplaceTypes()
        {
            IEnumerable<BasicViewModel> workplaceTypes = await this.nomenclatureService.GetWorkplaceTypes();

            return this.Ok(workplaceTypes);
        }
    }
}
