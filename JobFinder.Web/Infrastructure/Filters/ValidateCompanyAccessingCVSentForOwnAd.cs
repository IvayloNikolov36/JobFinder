using JobFinder.Services.CV;
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

            object cvId = context
                .ActionArguments
                .FirstOrDefault(aa => aa.Key == "cvId").Value;

            if (cvId == null)
            {
                context.Result = controller.BadRequest(new { Title = "The cv id is required!" });
                return;
            }

            object jobAdId = context
                .ActionArguments
                .FirstOrDefault(aa => aa.Key == "jobAdId").Value;

            if (jobAdId == null)
            {
                context.Result = controller.BadRequest(new { Title = "The job ad id is required!" });
                return;
            }

            string currentUserId = controller.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                await this.cvsService.ValidateCvIsSentForCurrentUsersJobAd(
                    cvId.ToString(),
                    int.Parse(jobAdId.ToString()),
                    currentUserId);
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
