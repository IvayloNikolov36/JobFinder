using JobFinder.Services;
using JobFinder.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace JobFinder.Web.Infrastructure.Filters
{
    public class ValidateAnonymousProfileBelongsToUser : ActionFilterAttribute
    {
        private readonly IAnonymousProfilesService anonymousProfileService;

        public ValidateAnonymousProfileBelongsToUser(IAnonymousProfilesService anonymousProfileService)
        {
            this.anonymousProfileService = anonymousProfileService;
        }

        public override async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            if (context.Controller is not ControllerBase controller)
            {
                return;
            }

            object id = context.GetParam("id");

            if (id == null)
            {
                context.SetBadRequestResult("Anonymous Profile id must be a string value!");
                return;
            }

            string requestUserId = controller.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string userId = await this.anonymousProfileService.GetOwnerId(id.ToString());

            if (userId == null)
            {
                context.Result = controller.NotFound("There is no anonymous profile with specified id!");
                return;
            }

            if (requestUserId != userId)
            {
                context.SetBadRequestResult("You can't access or modify other users anonymous profiles!");
                return;
            }

            await next();
        }
    }
}
