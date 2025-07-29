using AutoMapper;
using JobFinder.Data.Models;
using JobFinder.Web.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static JobFinder.Web.Infrastructure.WebConstants;

namespace JobFinder.Web.Controllers
{
    public class RegisterController : ApiController
    {
        private readonly UserManager<UserEntity> userManager;
        private readonly IMapper mapper;

        public RegisterController(
            UserManager<UserEntity> userManager,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("user")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegisterResult))]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterModel model)
        {
            UserEntity newUser = this.mapper.Map<UserEntity>(model);

            IdentityResult result = await this.userManager.CreateAsync(newUser, model.Password);

            RegisterResult requestResult = null;

            if (!result.Succeeded)
            {
                IEnumerable<string> errors = result.Errors.Select(x => x.Description);

                requestResult = new RegisterResult { Successful = false, Errors = errors };

                return this.BadRequest(requestResult);
            }

            IdentityResult addRoleResult = await this.userManager
                .AddToRoleAsync(newUser, JobSeekerRole);

            if (!addRoleResult.Succeeded)
            {
                return this.BadRequest(new RegisterResult { Errors = [CanNotAddJobSeekerRole] });
            }

            requestResult = new RegisterResult { Successful = true, Message = RegisterSuccess };

            return this.Ok(requestResult);
        }

        [HttpPost]
        [Route("company")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegisterResult))]
        public async Task<IActionResult> RegisterCompany([FromBody] RegisterCompanyModel model)
        {
            CompanyEntity newCompany = this.mapper.Map<CompanyEntity>(model);

            UserEntity newUser = this.mapper.Map<UserEntity>(model);
            newUser.Company = newCompany;

            IdentityResult result;
            try
            {
                result = await this.userManager.CreateAsync(newUser, model.Password);
            }
            catch (Exception)
            {
                return this.BadRequest(new RegisterResult { Errors = [ExistingCompany] });
            }

            if (!result.Succeeded)
            {
                IEnumerable<string> errors = result.Errors.Select(x => x.Description);

                return this.BadRequest(new RegisterResult { Errors = errors });
            }

            IdentityResult addRoleResult = await this.userManager.AddToRoleAsync(newUser, CompanyRole);
            if (!addRoleResult.Succeeded)
            {
                return this.BadRequest(new RegisterResult { Errors = [CanNotAddCompanyRole] });
            }

            return this.Ok(new RegisterResult { Successful = true, Message = RegisterSuccess });
        }
    }
}
