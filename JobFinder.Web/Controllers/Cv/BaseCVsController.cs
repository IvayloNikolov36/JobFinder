namespace JobFinder.Web.Controllers.Cv
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [ApiController]
    [Route("api/cvs/[controller]")]
    public abstract class BaseCVsController : ControllerBase
    {

    }
}
