using BankWebApp.Models;

namespace BankWebApp.ViewModels
{
    public class AccountTransactionsView
    {
        public string CustomerName { get; set; }
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public DateTime Created { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
