using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JobFinder.Web.Infrastructure.Extensions
{
    public static class ActionExecutingContextExtensions
    {
        public static object GetParam(this ActionExecutingContext context, string paramName)
        {
            return context
                .ActionArguments
                .FirstOrDefault(aa => aa.Key.ToLower().Equals(paramName)).Value;
        }

        public static void SetBadRequestResult(this ActionExecutingContext context, string errorMessage)
        {
            context.Result = (context.Controller as ControllerBase).BadRequest(new { Title = errorMessage });
        }

        public static ControllerParameterDescriptor GetBodyParameterDescriptor(this ActionExecutingContext context)
        {
            IEnumerable<ControllerParameterDescriptor> data = context
                .ActionDescriptor
                .Parameters
                .Select(p => p as ControllerParameterDescriptor);

            ControllerParameterDescriptor paramDescriptor = data
                .FirstOrDefault(p => p.ParameterInfo
                    .CustomAttributes.Any(attr => attr.AttributeType == typeof(FromBodyAttribute)));

            return paramDescriptor;
        }
    }
}
