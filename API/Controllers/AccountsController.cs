 using API.Services;
using BankWebApp.MobileApiModels;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetTransactions(int AccountId, int limit, int offset)
        {
            var transactions = _transactionService.GetTransactions(AccountId, limit, offset);
            if (transactions == null)
                return NotFound();
            //var JSON = new JsonResult(new { items = transactions });

            return Ok(transactions);
        }


    }
}
