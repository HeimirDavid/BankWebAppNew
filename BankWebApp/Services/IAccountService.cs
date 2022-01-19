using BankWebApp.Infrastructure.Paging;
using BankWebApp.Models;
using BankWebApp.ViewModels;

namespace BankWebApp.Services
{
    public interface IAccountService
    {
        public int NumberOfAllAccounts();

        public AccountViewModel GetAccount(int accountId, int customerId);
        public IEnumerable<TransactionViewModel> GetAllTransactions(int accountId, int pageNo);
    }
}
