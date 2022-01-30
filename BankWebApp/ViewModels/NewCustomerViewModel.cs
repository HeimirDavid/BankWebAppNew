using System.ComponentModel.DataAnnotations;

namespace BankWebApp.ViewModels
{
    public class NewCustomerViewModel
    {
        public int CustomerId { get; set; }
        [Required]
        public string Gender { get; set; } = null!;
        [Required]
        [StringLength(50)]
        public string Givenname { get; set; } = null!;
        [Required]
        [StringLength(50)]
        public string Surname { get; set; } = null!;
        [Required]
        [StringLength(100)]
        public string Streetaddress { get; set; } = null!;
        [Required]
        [StringLength(100)]
        public string City { get; set; } = null!; 
        [StringLength(10)]
        public string Zipcode { get; set; } = null!;
        [Required]
        [StringLength(100)]
        public string Country { get; set; } = null!;
        [StringLength(2)]
        public string? CountryCode { get; set; }
        //TODO Make so ago not under 13
        public DateTime? Birthday { get; set; }
        //TODO Use Richard regex for personalnumber
        public string? NationalId { get; set; }
        
        public string? Telephonecountrycode { get; set; }
        [StringLength(20)]
        public string? Telephonenumber { get; set; }
        [StringLength(150)]
        [EmailAddress]
        public string? Emailaddress { get; set; }

       
    }
}


