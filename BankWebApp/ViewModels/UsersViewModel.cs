using Microsoft.AspNetCore.Identity;

namespace BankWebApp.ViewModels
{
    public class UsersViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public IEnumerable<IdentityRole> Roles { get; set; }
        public int PageCount { get; set; }
    }
}
