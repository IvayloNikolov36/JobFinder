using JobFinder.Services;
using JobFinder.Web.Models.Common;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    }
}
