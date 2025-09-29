using JobFinder.Data.Models;
using JobFinder.Web.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using static JobFinder.Web.Infrastructure.WebConstants;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using JobFinder.Web.Infrastructure.Extensions;
using JobFinder.Services;

namespace JobFinder.Web.Controllers
{
    [Authorize]
    public class AccountController : ApiController
    {
        private readonly IConfiguration configuration;
        private readonly SignInManager<UserEntity> signInManager;
        private readonly UserManager<UserEntity> userManager;
        private readonly IMapper mapper;
        private readonly IAccountService accountService;

        public AccountController(
            IConfiguration configuration,
            SignInManager<UserEntity> signInManager,
            UserManager<UserEntity> userManager,
            IMapper mapper,
            IAccountService accountService)
        {
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.mapper = mapper;
            this.accountService = accountService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResult))]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            SignInResult result = await signInManager.PasswordSignInAsync(userName: model.Email, model.Password, false, false);

            if (!result.Succeeded)
            {
                return this.BadRequest(new { Title = InvalidEmailOrPassword });
            }

            UserEntity user = await this.userManager.FindByNameAsync(model.Email);
            IList<string> roles = await this.userManager.GetRolesAsync(user);

            List<Claim> claims = new(roles.Count + 1)
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(this.configuration["JwtSecurityKey"]));
            SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new(
                issuer: this.configuration["JwtIssuer"],
                audience: this.configuration["JwtAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(Convert.ToInt32(this.configuration["JwtExpiryInDays"])),
                signingCredentials: credentials
            );

            LoginResult loginResult = new()
            {
                Message = LoginSuccess,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Username = user.UserName,
                Id = user.Id,
                Roles = roles
            };

            return this.Ok(loginResult);
        }

        [HttpPost]
        [Route("register-user")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountResult))]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterModel model)
        {
            UserEntity newUser = this.mapper.Map<UserEntity>(model);

            IdentityResult result = await this.userManager.CreateAsync(newUser, model.Password);

            AccountResult requestResult = null;

            if (!result.Succeeded)
            {
                IEnumerable<string> errors = result.Errors.Select(x => x.Description);

                requestResult = new AccountResult(errors);

                return this.BadRequest(requestResult);
            }

            IdentityResult addRoleResult = await this.userManager
                .AddToRoleAsync(newUser, JobSeekerRole);

            if (!addRoleResult.Succeeded)
            {
                return this.BadRequest(new AccountResult([CanNotAddJobSeekerRole]));
            }

            requestResult = new AccountResult(RegisterSuccess);

            return this.Ok(requestResult);
        }

        [HttpPost]
        [Route("register-company")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountResult))]
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
                return this.BadRequest(new AccountResult([ExistingCompany]));
            }

            if (!result.Succeeded)
            {
                IEnumerable<string> errors = result.Errors.Select(x => x.Description);

                return this.BadRequest(new AccountResult(errors));
            }

            IdentityResult addRoleResult = await this.userManager.AddToRoleAsync(newUser, CompanyRole);
            if (!addRoleResult.Succeeded)
            {
                return this.BadRequest(new AccountResult([CanNotAddCompanyRole]));
            }

            return this.Ok(new AccountResult(RegisterSuccess));
        }

        [HttpPost]
        [Route("change-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            string userId = this.User.GetCurrentUserId();

            IdentityResult result = await this.accountService.ChangePassword(model, userId);

            if (!result.Succeeded)
            {
                return this.BadRequest(new AccountResult(result.Errors.Select(e => e.Description)));
            }

            return this.Ok();
        }

        [HttpPost]
        [Route("forgotten-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgottenPassword([FromBody] ForgottenPasswordModel model) 
        {
            await this.accountService.SendPasswordResetLink(model);

            return this.Ok();
        }
    }
}
