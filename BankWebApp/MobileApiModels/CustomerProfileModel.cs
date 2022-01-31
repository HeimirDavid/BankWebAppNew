using Microsoft.AspNetCore.Identity;

namespace BankWebApp.MobileApiModels
{
    public class CustomerProfileModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
