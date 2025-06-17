using JobFinder.Services.Cv;
using JobFinder.Web.Models.AdApplication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace JobFinder.Web.Infrastructure.Filters
{
    public class ValidateCompanyAccessingCVSentForOwnAd : ActionFilterAttribute
    {
        private readonly ICvsService cvsService;

        public ValidateCompanyAccessingCVSentForOwnAd(ICvsService cvsService)
        {
            this.cvsService = cvsService;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.Controller is not ControllerBase controller)
            {
                return;
            }


            if (context.ActionArguments.First().Value is not ApplicationPreviewInfoInputModel model)
            {
                return;
            }

            string cvId = model.CvId;

            if (string.IsNullOrEmpty(cvId.Trim()))
            {
                context.Result = controller.BadRequest(new { Title = "The cv id is required!" });
                return;
            }

            int jobAdId = model.JobAdId;

            if (jobAdId < 1)
            {
                context.Result = controller.BadRequest(new { Title = "Invalid Job Ad Id!" });
                return;
            }

            string currentUserId = controller.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                await this.cvsService
                    .ValidateApplicationIsSentForCurrentUserJobAd(cvId, jobAdId, currentUserId);
            }
            catch (Exception ex)
            {
                context.Result = controller.BadRequest(ex.Message);
                return;
            }

            await next();
        }
    }
}
