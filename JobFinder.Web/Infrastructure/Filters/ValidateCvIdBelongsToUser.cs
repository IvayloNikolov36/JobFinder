﻿using JobFinder.Services.Cv;
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
            string userId = await this.cvsService.GetOwnerId(id.ToString());

            if (userId == null)
            {
                context.Result = controller.NotFound("There is no CV with specified id!");
                return;
            }

            if (requestUserId != userId)
            {
                context.Result = controller.BadRequest(new { Title = "You can't access or modify other users CVs!" });
                return;
            }

            await next();
        }
    }
}
