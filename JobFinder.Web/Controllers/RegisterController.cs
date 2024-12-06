namespace JobFinder.Web.Controllers
{
    using JobFinder.Data.Models;
    using JobFinder.Web.Models.Account;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;
    using JobFinder.Data;
    using static JobFinder.Web.Infrastructure.WebConstants;
    using System;

    public class RegisterController : ApiController
    {
        private readonly UserManager<UserEntity> userManager;
        private readonly JobFinderDbContext dbContext;

        public RegisterController(UserManager<UserEntity> userManager, JobFinderDbContext dbContext)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        [HttpPost("user")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterModel model)
        {
            var newUser = new UserEntity 
            { 
                UserName = model.Username, 
                Email = model.Email,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName
            };

            var result = await this.userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);

                return BadRequest(new RegisterResult { Successful = false, Errors = errors });
            }

            return Ok(new RegisterResult { Successful = true, Message = "Successfully registered!" } );
        }

        [HttpPost("company")]
        public async Task<IActionResult> RegisterCompany([FromBody] RegisterCompanyModel model)
        {
            var newCompany = new CompanyEntity
            {
                Name = model.CompanyName,
                Bulstat = model.Bulstat,
                Logo = model.CompanyLogo
            };
            
            var newUser = new UserEntity
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                Company = newCompany
            };

            IdentityResult result;
            try
            {
                 result = await this.userManager.CreateAsync(newUser, model.Password);
            }
            catch (Exception)
            {
                return this.BadRequest(new { Title = "There is already a company with this name or bulstat!" });
            }
            
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);
                return BadRequest(new RegisterResult { Successful = false, Errors = errors });
            }

            IdentityResult adToRoleResult = await this.userManager.AddToRoleAsync(newUser, CompanyRole);
            if (!adToRoleResult.Succeeded)
            {
                return BadRequest(new RegisterResult { Successful = false });
            }

            return Ok(new RegisterResult { Successful = true, Message = "Successfully registered!" });
        }
    }
}
