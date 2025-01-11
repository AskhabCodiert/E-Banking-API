using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Banking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet("{id}")]
        public IActionResult GetTransactionByCustomerId(int customerId)
        {
            var transactions = _context.Transactions
         .Where(t => _context.Accounts
             .Where(a => a.CustomerID == customerId)
             .Select(a => a.AccountID)
             .Contains(t.AccountID))
         .ToList();

            return Ok(transactions);
        }

    }
}
