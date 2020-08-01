namespace JobFinder.Web.Controllers
{
    using JobFinder.Services;
    using JobFinder.Web.Models.JobAds;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class JobCategoriesController : ApiController
    {
        private readonly IJobCategoriesService categoriesService;

        public JobCategoriesController(IJobCategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobCategoryViewModel>>> GetCategories()
        {
            var categories = await this.categoriesService.AllAsync<JobCategoryViewModel>();

            return this.Ok(categories);
        }
    }
}
