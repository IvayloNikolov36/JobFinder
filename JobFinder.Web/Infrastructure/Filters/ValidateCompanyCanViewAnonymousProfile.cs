using JobFinder.Services;
using JobFinder.Web.Infrastructure.Extensions;
using JobFinder.Web.Models.JobAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JobFinder.Web.Infrastructure.Filters;

public class ValidateCompanyCanViewAnonymousProfile : ActionFilterAttribute
{
    private readonly IJobAdsService jobAdService;
    private readonly IAnonymousProfilesService anonymousProfileService;

    public ValidateCompanyCanViewAnonymousProfile(
        IJobAdsService jobAdService,
        IAnonymousProfilesService anonymousProfileService)
    {
        this.jobAdService = jobAdService;
        this.anonymousProfileService = anonymousProfileService;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.Controller is not ControllerBase controller)
        {
            return;
        }

        object id = context.GetParam("id");
        object jobAdId = context.GetParam("jobadid");

        if (id == null && jobAdId == null)
        {
            ControllerParameterDescriptor paramDescriptor = context.GetBodyParameterDescriptor();

            if (paramDescriptor is null)
            {
                context.SetBadRequestResult("The anonymous profile id and jobAdId must be provided!");
                return;
            }

            dynamic entity = context.ActionArguments[paramDescriptor.Name];
            id = entity.AnonymousProfileId;
            jobAdId = entity.JobAdId;
        }
        else
        {
            if (id == null)
            {
                context.SetBadRequestResult("The anonymous profile id must be provided!");
                return;
            }

            if (jobAdId == null)
            {
                context.SetBadRequestResult("Job Ad id must be provided!");
                return;
            }
        }

        JobAdCriteriasViewModel jobAdCriterias = await this.jobAdService
            .GetJobAdCriterias((int)jobAdId);

        bool isRelevant = await this.anonymousProfileService
            .IsRelevant(((Guid)id).ToString(), jobAdCriterias);

        if (!isRelevant)
        {
            context.SetBadRequestResult("This anonymous profile is not relevant to the specified job ad!");
            return;
        }

        await next();
    }
}
