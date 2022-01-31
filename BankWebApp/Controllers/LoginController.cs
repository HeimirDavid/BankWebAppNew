using BankWebApp.MobileApiModels;
using BankWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;

        private readonly SignInManager<User> _signInManager;
        private readonly BankContext _context;

        public LoginController(IConfiguration config, SignInManager<User> signInManager, BankContext context)
        {
            _config = config;
            _signInManager = signInManager;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginModel userLogin)
        {
            var user = await Authenticate(userLogin);


            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }

            return NotFound("User not found");
        }


        private string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //var role = await _userManager.GetRolesAsync(user);

            var claims = new[]
           {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                //new Claim(ClaimTypes.GivenName, user.Customer.Givenname),
                //new Claim(ClaimTypes.Surname, user.Customer.),
                //new Claim(ClaimTypes.Role, role),
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<User> Authenticate(UserLoginModel userLogin)
        {

            //var currentUser = _context.Users.FirstOrDefault(o => o.UserName.ToLower() == userLogin.Username.ToLower() && o.PasswordHash == userLogin.Password);
            var result = await _signInManager.PasswordSignInAsync(userLogin.Username,
                           userLogin.Password, false, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                var user = _context.Users.First(u => u.UserName == userLogin.Username);
                return user;
            }

            return null;
        }
    }
}

