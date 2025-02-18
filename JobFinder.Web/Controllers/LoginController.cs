namespace JobFinder.Web.Controllers
{
    using JobFinder.Data.Models;
    using JobFinder.Web.Models.Account;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

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
            SignInResult result = await signInManager
                .PasswordSignInAsync(userName: model.Username, model.Password, false, false);

            if (!result.Succeeded)
            {
                return this.BadRequest(new { Title = "Username and/or password are invalid." });
            }

            UserEntity user = await this.userManager.FindByNameAsync(model.Username);
            IList<string> roles = await this.userManager.GetRolesAsync(user);

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(configuration["JwtSecurityKey"]));
            SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new(
                issuer: configuration["JwtIssuer"],
                audience: configuration["JwtAudience"],
                claims: claims,
                expires: DateTime.Now.AddDays(Convert.ToInt32(configuration["JwtExpiryInDays"])),
                signingCredentials: credentials
            );

            LoginResult loginResult = new()
            {
                Message = "Successfully logged in!",
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Username = user.UserName,
                Id = user.Id,
                Role = roles.Any() ? roles.First() : string.Empty,
            };

            return this.Ok(loginResult);
        }
    }
}
