

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
        TransactionError Deposit(int AccountIId, decimal Amount, DateTime DateWhen);
        TransactionError Withdraw(int AccountIId, decimal Amount);

    }
}
