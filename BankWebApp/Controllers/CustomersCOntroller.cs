using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {

        [HttpGet("GetAllCustomers")]
        public IEnumerable<string> GetCustomers()
        {
            return new[] { "value1", "value2" };
        }
    }
}
