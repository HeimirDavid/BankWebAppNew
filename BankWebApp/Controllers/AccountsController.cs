using BankWebApp.MobileApiModels;
using BankWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankWebApp.Controllers
{
   


    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public AccountsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        //[Authorize]
        public IEnumerable<TransactionsAPIModel> GetTransactions(int AccountId, int limit, int offset)
        {
            var transactions = _transactionService.GetTransactions(AccountId, limit, offset);

            var JSON = new JsonResult(new { items = transactions });

            return transactions;
        }


    }
}
