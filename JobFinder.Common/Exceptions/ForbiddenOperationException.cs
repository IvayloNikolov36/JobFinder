﻿using JobFinder.Common.Enums;
using System;

namespace JobFinder.Common.Exceptions
{
    public class ForbiddenOperationException : Exception
    {
        private const string DefaultCaption = "Forbidden";

        public ForbiddenOperationException(
            SeverityEnum severity,
            string message,
            string caption = DefaultCaption,
            Exception innerException = null) : base (message, innerException)
        {
            this.Severity = severity;
            this.Caption = caption;            
        }

        public SeverityEnum Severity { get; private set; }

        public string Caption { get; private set; }
    }
}
