using JobFinder.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data
{
    public class JobFinderDbContext : IdentityDbContext<User>
    {
        public JobFinderDbContext(DbContextOptions<JobFinderDbContext> options) 
            : base(options)
        {
        }
    }
}
