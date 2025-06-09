using JobFinder.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace JobFinder.Web.Infrastructure.Filters
{
    public class ValidateAnonymousProfileBelongsToUser : ActionFilterAttribute
    {
        private readonly IAnonymousProfileService anonymousProfileService;

        public ValidateAnonymousProfileBelongsToUser(IAnonymousProfileService anonymousProfileService)
        {
            this.anonymousProfileService = anonymousProfileService;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ControllerBase controller = context.Controller as ControllerBase;

            if (controller == null)
            {
                return;
            }

            object id = context
                .ActionArguments
                .FirstOrDefault(aa => aa.Key.ToLower().Contains("id")).Value;

            if (id == null)
            {
                context.Result = controller.BadRequest(new { Title = "The CV id must be a string value!" });
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
                context.Result = controller.BadRequest(new { Title = "You can't access or modify other users anonymous profiles!" });
                return;
            }

            await next();
        }
    }
}
