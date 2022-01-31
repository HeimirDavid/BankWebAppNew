using BankWebApp.MobileApiModels;
using BankWebApp.Models;
using BankWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IUserService _userService;
        public CustomersController(IUserService userService)
        {
            _userService = userService;
        }



        [Authorize(Roles = "Customer")]
        [HttpGet("Profile")]
        public CustomerProfileModel GetCustomerProfile()
        {
            var currentUser = GetCurrentUser();
            if (currentUser == null)
                return null;

            var customerProfile = _userService.GetUserForApi(currentUser.Email);


            return customerProfile;
        }


        private User GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new User
                {
                    UserName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                };
            }
            return null;
        }
    }
}
