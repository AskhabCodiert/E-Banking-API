using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Banking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/<AccountsController>/5
        [HttpGet("{id}")]
        public IActionResult GetAccountById(int id)
        {
            var account = _context.Accounts.FirstOrDefault(a => a.AccountID == id);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        // POST api/<AccountsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AccountsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AccountsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
