using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Data.Models
{
    public class User : IdentityUser
    {
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [StringLength(25, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string LastName { get; set; }

    }
}
