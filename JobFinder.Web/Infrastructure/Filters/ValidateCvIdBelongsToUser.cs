namespace JobFinder.Web.Infrastructure.Filters
{
    using JobFinder.Services.CV;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class ValidateCvIdBelongsToUser : ActionFilterAttribute
    {
        private readonly ICVsService cvsService;

        public ValidateCvIdBelongsToUser(ICVsService cvsService)
        {
            this.cvsService = cvsService;
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
            }
            else
            {
                string requestUserId = controller.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                string userId = await this.cvsService.GetOwnerId(id.ToString());

                if (requestUserId != userId)
                {
                    context.Result = controller.BadRequest(new { Title = "You can't access or modify other users CVs!" });
                }

                await next();
            }
      
        }
    }
}
