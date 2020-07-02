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

    public class LoginController : ApiController
    {
        private readonly IConfiguration configuration;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public LoginController(IConfiguration configuration,
                               SignInManager<User> signInManager,
                               UserManager<User> userManager)
        {
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await signInManager.PasswordSignInAsync(userName: model.Username, model.Password, false, false);

            if (!result.Succeeded)
            {
                return BadRequest(new { Title = "Username and/or password are invalid." });
            }

            var user = await this.userManager.FindByNameAsync(model.Username);
            var roles = await this.userManager.GetRolesAsync(user);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(Convert.ToInt32(configuration["JwtExpiryInDays"]));

            var token = new JwtSecurityToken(
                configuration["JwtIssuer"],
                configuration["JwtAudience"],
                claims,
                expires: expiry,
                signingCredentials: creds
            );

            return Ok(new LoginResult
            {
                Message = "Successfully logged in!",
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Username = user.UserName,
                Id = user.Id,
                Role = roles.Any() ? roles.First() : string.Empty,
            });
        }
    }
}
