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

namespace JobFinder.Web.Controllers
{
    public class LoginController : ApiController
    {
        private readonly IConfiguration configuration;
        private readonly SignInManager<UserEntity> signInManager;
        private readonly UserManager<UserEntity> userManager;

        public LoginController(
            IConfiguration configuration,
            SignInManager<UserEntity> signInManager,
            UserManager<UserEntity> userManager)
        {
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpPost]
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
    }
}
