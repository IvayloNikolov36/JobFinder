using JobFinder.Services;
using JobFinder.Web.Models.JobAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JobFinder.Web.Infrastructure.Filters;

public class ValidateCompanyCanViewAnonymousProfile : ActionFilterAttribute
{
    private readonly IJobAdsService jobAdService;
    private readonly IAnonymousProfileService anonymousProfileService;

    public ValidateCompanyCanViewAnonymousProfile(
        IJobAdsService jobAdService,
        IAnonymousProfileService anonymousProfileService)
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

        object id = context
            .ActionArguments
            .FirstOrDefault(aa => aa.Key.ToLower().Equals("id")).Value;

        if (id == null)
        {
            context.Result = controller.BadRequest(new { Title = "The anonymous profile id must be provided!" });
            return;
        }

        object jobAdId = context
            .ActionArguments
            .FirstOrDefault(aa => aa.Key.ToLower().Equals("jobadid")).Value;

        if (jobAdId == null)
        {
            context.Result = controller.BadRequest(new { Title = "Job Ad id must be provided!" });
            return;
        }

        int jobAdIdValue = (int)jobAdId;
        Guid anonymousProfileGuid = (Guid)id;

        JobAdCriteriasViewModel jobAdCriterias = await this.jobAdService
            .GetJobAdCriterias(jobAdIdValue);

        bool isRelevant = await this.anonymousProfileService
            .IsRelevant(anonymousProfileGuid.ToString(), jobAdCriterias);

        if (!isRelevant)
        {
            context.Result = controller.BadRequest(new { Title = "This anonymous profile is not relevant to the specified job ad!" });
            return;
        }

        await next();
    }
}
