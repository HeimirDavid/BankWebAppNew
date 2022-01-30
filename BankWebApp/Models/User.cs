using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace BankWebApp.Models
{
    public partial class User : IdentityUser
    {
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        

    }


}
