using BankWebApp.Services;
using BankWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankWebApp.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class UserModel : PageModel
    {
        private readonly IUserService _userService;

        public UserViewModel userViewModel { get; set; }

        public UserModel(IUserService userService)
        {
            _userService = userService;
        }
        public void OnGet(string userId)
        {
            userViewModel = _userService.GetUser(userId);
        }
    }
}
