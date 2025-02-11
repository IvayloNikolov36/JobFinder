using JobFinder.Common.Enums;
using System;

namespace JobFinder.Common.Exceptions
{
    public class UnauthorizedException : ForbiddenOperationException
    {
        public UnauthorizedException(string message, Exception innerException = null)
            : base(SeverityEnum.Warning, message, innerException: innerException)
        {

        }
    }
}
