using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BlogApi.Entities;
using BlogApi.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/[controllerx|]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManagaer;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticateController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _configuration = configuration;
            _userManagaer = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var user = await _userManagaer.FindByEmailAsync(loginModel.Email);
            if (user != null && await _userManagaer.CheckPasswordAsync(user, loginModel.Password))
            {
                var userRoles = await _userManagaer.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())

                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiratio = token.ValidTo
                });
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            var userExist = _userManagaer.FindByEmailAsync(registerModel.Email)
          if (userExist != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseServer { Status = "Error", Message = "User already Exist!" });

            ApplicationUser User = new ApplicationUser()
            {
                Email = registerModel.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerModel.Name
            };

            var result = await _userManagaer.CreateAsync(User, registerModel.Password);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseServer { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return Ok(new ResponseServer { Status = "Success", Message = "User created succesfully" });
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin(RegisterModel registerModel)
        {
            vat = _userManaga er.FindByEmailAsync(registerModel.Email);
            if (userExist != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseServer { Status = "Error", Message = "User already Exist!" });

                   ApplicationUser User = new ApplicationUser()
            {
                Email = registerModel.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerModel.Name
            };

            var result = await _userManagaer.CreateAsync(User, registerModel.Password);

            if (!result. Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseServer { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (! await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
 
            if(await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManagaer.AddToRoleAsync(User, UserRoles.Admin);
            }

            return Ok(new ResponseServer { Status = "Success", Message = "User created succesfully" });
        }
    }
}