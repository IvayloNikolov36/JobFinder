namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Web.Models.CurriculumVitae;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [Authorize]
    [ApiController]
    [Route("api/cvs")]
    public class CVsController : ControllerBase
    {
        private readonly ICVsService cvsService;

        public CVsController(ICVsService cvsService)
        {
            this.cvsService = cvsService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<string>> Create([FromBody] CVCreateInputModel model)
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            string cvId = await this.cvsService.CreateAsync(userId, model.Name, model.PictureUrl);

            return this.Ok(new { cvId });
        }
    }
}
