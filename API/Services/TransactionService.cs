using BankWebApp.MobileApiModels;
using BankWebApp.Models;

namespace API.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly BankContext _context;

        public TransactionService(BankContext context)
        {
            _context = context;
        }


        public IEnumerable<TransactionsAPIModel> GetTransactions(int AccountId, int limit, int offset)
        {


            var query = _context.Transactions

                .Where(a => a.AccountId == AccountId)
                .OrderByDescending(a => a.Date)
                .Skip(offset)
                .Take(limit)
                .Select(a => new TransactionsAPIModel
                {
                    AccountId = a.AccountId,
                    TransactionId = a.TransactionId,
                    Date = a.Date,
                    Type = a.Type,
                    Operation = a.Operation,
                    Amount = a.Amount,
                    Balance = a.Balance
                });


            return query;
        }
    }
}
