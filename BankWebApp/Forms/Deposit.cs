using System.ComponentModel.DataAnnotations;

namespace BankWebApp.Forms
{
    public class Deposit
    {
        [Range(1 ,15000)]
        public DateTime DateWhen { get; set; }
        public decimal Amount { get; set; }

    }
}
