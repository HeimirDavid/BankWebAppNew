using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BankWebApp.ViewModels
{
    public class EditUserViewModel
    {
        [Required]
        public string Id { get; set; }


        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }


        [Display(Name = "Admin")]
        public bool AdminRole { get; set; }

        [Display(Name = "Cashier")]
        public bool CashierRole { get; set; }

    }
}
