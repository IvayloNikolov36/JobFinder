using JobFinder.Services;
using JobFinder.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace JobFinder.Web.Infrastructure.Filters;

public class ValidateJobAdBelongsToUser : ActionFilterAttribute
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

        object id = context.GetParam("jobadid");

        if (id is null)
        {
            ControllerParameterDescriptor paramDescriptor = context.GetBodyParameterDescriptor();

            if (paramDescriptor is null)
            {
                context.SetBadRequestResult("The anonymous profile id and jobAdId must be provided!");
                return;
            }

            dynamic entity = context.ActionArguments[paramDescriptor.Name];
            id = entity.JobAdId;
        }

        if (id is not int jobAdId)
        {
            context.SetBadRequestResult("JobAd id must be a valid integer number!");
            return;
        }

        string requestUserId = controller.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        string jobAdPublisherId = await this.jobAdService.GetPublisherId(jobAdId);

        if (jobAdPublisherId != requestUserId)
        {
            context.SetBadRequestResult("You are not allowed to modify other users jobAds!");
            return;
        }

        await next();
    }
}
