

using BankWebApp.MobileApiModels;
using BankWebApp.Models;

namespace BankWebApp.Services
{
    public interface ITransactionService
    {
        Tuple<int, int> GetWeek(DateTime d);
        public DateTime[] GetWeekDays(int Year, int WeekNumber);
        IEnumerable<Transaction> GetThisWeek();
        IEnumerable<Transaction> GetLastWeek();
        IEnumerable<Transaction> GetAll();

        public enum TransactionError
        {
            Ok,
            BalanceTooLow,
            AmountTooHigh,
            InvalidAccount,
            InvalidDate,
        }

        TransactionError Deposit(int AccountIId, decimal Amount);

        TransactionError Withdraw(int AccountIId, decimal Amount);

        //public IEnumerable<TransactionsAPIModel> GetTransactions(int AccountId);

        public IEnumerable<TransactionsAPIModel> GetTransactions(int AccountId, int limit, int offset);


    }
}
