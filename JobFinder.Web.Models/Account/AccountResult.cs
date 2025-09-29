using System.Collections.Generic;

namespace JobFinder.Web.Models.Account;

public class AccountResult
{
    public AccountResult(string message)
    {
        this.Message = message;
    }

    public AccountResult(IEnumerable<string> errors)
    {
        this.Errors = errors;
    }

    public string Message { get; set; } = string.Empty;

    public IEnumerable<string> Errors { get; set; } = [];
}
