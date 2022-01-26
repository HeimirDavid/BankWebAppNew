using Microsoft.AspNetCore.Identity;

namespace BankWebApp.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set;}
        public string Email { get; set;}
        public string PhoneNumber { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
