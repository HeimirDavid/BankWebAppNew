using BankWebApp.Services;
using BankWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BankWebApp.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly IUserService _userService;

        public EditModel(IUserService userService)
        {
            _userService = userService;
        }
        [BindProperty]
        public EditUserViewModel editUserViewModel { get; set; }


        //public bool GetAdminRole { get; set; }


        //public bool GetCashierRole { get; set; }


        //[BindProperty]
        //[Display(Name = "Admin")]
        //public bool AdminRole { get; set; }

        //[BindProperty]
        //[Display(Name = "Cashier")]
        //public bool CashierRole { get; set; }

        public void OnGet(string userId)
        {

            editUserViewModel = _userService.EditUserOnGet(userId);

            foreach (var role in editUserViewModel.Roles)
            {
                if (role.Name == "Admin")
                {
                    editUserViewModel.AdminRole = true;
                }
                else if (role.Name == "Cashier")
                {
                    editUserViewModel.CashierRole = true;
                }
            }


        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                if (editUserViewModel.AdminRole)
                {
                    
                }

                //_userService.UpdateUser(editUserViewModel);
            }

        }
    }
}
