﻿namespace JobFinder.Web.Models.Account
{
    using System.Collections.Generic;

    public class RegisterResult
    {
        public bool Successful { get; set; } = false;

        public string Message { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}
