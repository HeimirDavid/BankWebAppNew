using BankWebApp.Models;

namespace BankWebApp.Services
{
    public class SaldoService : ISaldoService
    {
        private readonly BankContext _context;

        public SaldoService(BankContext bankContext)
        {
            _context = bankContext;
        }
        public decimal TotalOfAllSaldo()
        {
            var accounts = _context.Accounts;
            decimal total = 0;
            foreach (var account in accounts)
            {
                total = total + account.Balance;
            }

            return total;
        }
    }
}
