using BankWebApp.MobileApiModels;

namespace API.Services
{
    public interface ITransactionService
    {
        public IEnumerable<TransactionsAPIModel> GetTransactions(int AccountId, int limit, int offset);
    }
}
