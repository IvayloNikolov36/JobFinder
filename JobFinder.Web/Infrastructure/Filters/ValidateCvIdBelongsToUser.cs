using JobFinder.Services.Cv;
using JobFinder.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace JobFinder.Web.Infrastructure.Filters
{
    public class ValidateCvIdBelongsToUser : ActionFilterAttribute
    {
        private readonly ICvsService cvsService;

        public ValidateCvIdBelongsToUser(ICvsService cvsService)
        {
            this.cvsService = cvsService;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.Controller is not ControllerBase controller)
            {
                return;
            }

            object id = context.GetParam("cvId");

            if (id == null)
            {
                context.SetBadRequestResult("The CV id must be a string value!");
                return;
            }

            string requestUserId = controller.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string userId = await this.cvsService.GetOwnerId(id.ToString());

            if (userId == null)
            {
                context.Result = controller.NotFound("There is no CV with specified id!");
                return;
            }

            if (requestUserId != userId)
            {
                context.SetBadRequestResult("You can't access or modify other users CVs!");
                return;
            }

            await next();
        }
    }
}
