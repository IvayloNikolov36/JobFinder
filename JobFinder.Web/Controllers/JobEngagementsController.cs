namespace JobFinder.Web.Controllers
{
    using JobFinder.Services;
    using JobFinder.Web.Models.JobAds;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class JobEngagementsController : ApiController
    {
        private readonly IJobEngagementsService engagementsService;

        public JobEngagementsController(IJobEngagementsService engagementsService)
        {
            this.engagementsService = engagementsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobEngagementViewModel>>> All()
        {
            var engagements = await this.engagementsService
                .AllAsync<JobEngagementViewModel>();

            return this.Ok(engagements);
        }
    }
}
