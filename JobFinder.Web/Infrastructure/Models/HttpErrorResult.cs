using JobFinder.Common.Enums;
using JobFinder.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JobFinder.Web.Infrastructure.Models
{
    public class HttpErrorResult
    {
        private const string ActionableErrorMessage = "Your request could not be completed!";
        private const string UnauthorizedErrorMessage = "You are not authorized to access this page or resource!";
        private const string UnknownErrorMessage = "An unexpected situation occurred! The dev team would be notified!";

        private HttpErrorResult(Guid guid, string message)
        {
            this.Guid = guid;
            this.Message = message;
            this.Errors = new List<string>();
        }

        public HttpErrorResult(Guid guid)
            : this(guid, UnknownErrorMessage)
        {

        }

        public HttpErrorResult(Guid guid, ActionableException exception)
            : this(guid, ActionableErrorMessage)
        {
            this.Severity = SeverityEnum.Error;
            this.PopulateErrors(exception);
            this.Errors = this.Errors.Reverse().ToList();
        }

        public HttpErrorResult(Guid guid, UnauthorizedException exception)
            : this(guid, exception.Caption)
        {
            this.Severity = exception.Severity;
            this.Errors = new List<string>(1) { UnauthorizedErrorMessage };
        }

        public Guid Guid { get; private set; }

        public string Message { get; private set; }

        public SeverityEnum Severity { get; private set; }

        public ICollection<string> Errors { get; private set; }

        private void PopulateErrors<TException>(TException exception) where TException : Exception
        {
            if (exception.InnerException is TException)
            {
                this.PopulateErrors(exception.InnerException as TException);
            }

            this.Errors.Add(exception.Message);
        }
    }
}
