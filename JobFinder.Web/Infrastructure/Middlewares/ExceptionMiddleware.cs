using JobFinder.Common.Exceptions;
using JobFinder.Web.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;

namespace JobFinder.Web.Infrastructure.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        // TODO: create logger and inject it in the constructor
        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception ex)
            {
                await this.HandleExceptionAsync(context, ex, Guid.NewGuid()); // 
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception, Guid guid)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            //ControllerActionDescriptor controllerActionDescriptor = context
            //    .GetEndpoint()
            //    .Metadata
            //    .GetMetadata<ControllerActionDescriptor>();

            //string controllerName = controllerActionDescriptor.ControllerName;
            //string actionName = controllerActionDescriptor.ActionName;
            //IDictionary<string, string> routeValues = controllerActionDescriptor.RouteValues;

            HttpErrorResult errorResult = null;

            switch (exception)
            {
                case ActionableException actionableException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResult = new HttpErrorResult(guid, actionableException);
                    break;
                case UnauthorizedException unauthorizedException:
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    errorResult = new HttpErrorResult(guid, unauthorizedException);
                    break;
                default:
                    errorResult = new HttpErrorResult(guid);
                    break;
            }

            JsonSerializerOptions options = new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            string jsonResult = JsonSerializer.Serialize(errorResult, options);

            return context.Response.WriteAsync(jsonResult);
        }
    }
}
