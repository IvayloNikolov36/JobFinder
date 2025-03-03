using JobFinder.Services.CV;
using JobFinder.Web.Models.JobAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JobFinder.Web.Infrastructure.Filters
{
    public class ValidateCompanyAccessingCVSentForOwnAd : ActionFilterAttribute
    {
        private readonly ICVsService cvsService;

        public ValidateCompanyAccessingCVSentForOwnAd(ICVsService cvsService)
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
                context.Result = controller.BadRequest(new { Title = "The job advertisement id is not correct!" });
                return;
            }

            string currentUserId = controller.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                await this.cvsService
                    .ValidateCvIsSentForCurrentUsersJobAd(cvId, jobAdId, currentUserId);
            }
            catch (UnauthorizedAccessException ex)
            {
                context.Result = controller.BadRequest(ex.Message);
                return;
            }

            await next();
        }
    }
}
