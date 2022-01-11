using BankWebApp.Models;
using BankWebApp.ViewModels;

namespace BankWebApp.Services
{
    public interface IAccountService
    {
        public int NumberOfAllAccounts();

        public AccountTransactionsView GetAccountAndTransactions(int accountId, int customerId);
    }
}
