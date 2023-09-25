using APITask.DTOS;
using APITask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APITask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterDTO newUserDto)
        {
            ApplicationUser appUser = new ApplicationUser();
            appUser.UserName = newUserDto.Username;
            appUser.PasswordHash = newUserDto.Password;
            appUser.Address = newUserDto.Address;
            IdentityResult result =
                await userManager.CreateAsync(appUser, appUser.PasswordHash);
            if (result.Succeeded)
                return Ok("Created");
            else
                return BadRequest(result.Errors.ToList());
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginDTO newUser, [FromServices] IConfiguration config)
        {
            ApplicationUser appUserModel = await userManager.FindByNameAsync(newUser.UserName);
            if (appUserModel != null)
            {
                bool found = await userManager.CheckPasswordAsync(appUserModel, newUser.Password);
                if (found)
                {
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, newUser.UserName));
                    claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                    var symKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]));
                    var signInCredentials = new SigningCredentials(symKey, SecurityAlgorithms.HmacSha256);
                    JwtSecurityToken UserToken = new JwtSecurityToken(
                        issuer: config["JWT:Issues"],
                        audience: config["JWT:Audiance"],
                        expires: DateTime.Now.AddHours(1),
                        claims: claims,
                        signingCredentials: signInCredentials
                        );
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(UserToken),
                        expiration = UserToken.ValidTo
                    });
                }
            }
            return Unauthorized("Invalid Account");
        }
    }
}
