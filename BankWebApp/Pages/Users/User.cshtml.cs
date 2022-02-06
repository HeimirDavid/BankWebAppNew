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
        public string EditSuccess { get; set; }


        public UserModel(IUserService userService)
        {
            _userService = userService;
        }
        public void OnGet(string userId, bool edit)
        {
            userViewModel = _userService.GetUser(userId);
            if (edit)
            {
                EditSuccess = "User updated!";
            }
        }
    }
}
