using JobFinder.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace JobFinder.Web.Infrastructure.Filters;

public class 
    ValidateJobAdBelongsToUser : ActionFilterAttribute
{
    private readonly IJobAdsService jobAdService;

    public ValidateJobAdBelongsToUser(IJobAdsService jobAdService)
    {
        this.jobAdService = jobAdService;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.Controller is not ControllerBase controller)
        {
            return;
        }

        object id = context
            .ActionArguments
            .FirstOrDefault(aa => aa.Key.ToLower().Equals("jobadid")).Value;

        if (id is not int jobAdId)
        {
            context.Result = controller.BadRequest(new { Title = "JobAd id must be a valid integer number!" });
            return;
        }

        string requestUserId = controller.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        string jobAdPublisherId = await this.jobAdService.GetPublisherId(jobAdId);

        if (jobAdPublisherId != requestUserId)
        {
            context.Result = controller.BadRequest(new { Title = "You are not allowed to modify other users jobAds!" });
            return;
        }

        await next();
    }
}
