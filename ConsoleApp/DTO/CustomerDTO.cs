using BankWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.DTO
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }
        public string Country { get; set; }

        public Account Account { get; set; }
    }
}
