namespace JobFinder.Web.Infrastructure.Filters
{
    using JobFinder.Services.CurriculumVitae;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System.Linq;

    public class ValidateCvIdExistsServiceFilter : ActionFilterAttribute
    {
        private readonly ICVsService cvsService;

        public ValidateCvIdExistsServiceFilter(ICVsService cvsService)
        {
            this.cvsService = cvsService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as ControllerBase;
            if (controller == null)
            {
                return;
            }

            var id = context.ActionArguments
                .FirstOrDefault(a => a.Key.ToLower().Contains("id")).Value;

            if (id == null)
            {
                context.Result = controller.BadRequest(new { Title = "Not existing CV Id!" } );
            }
            else
            {
                var exists = this.cvsService.ExistsAsync(id.ToString()).GetAwaiter().GetResult();

                if (!exists)
                {
                    context.Result = controller.BadRequest(new { Title = "Not existing CV Id!" });
                }
            }

        }
    }
}
