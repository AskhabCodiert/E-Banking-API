using E_Banking_API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace E_Banking_API.Controllers
{
    public class UsersController : Controller
    {
        [Route("api/Customer")]
        [ApiController]
        public class CustomerController : ControllerBase
        {
            private readonly ApplicationDbContext _context;

            public CustomerController(ApplicationDbContext context)
            {
                _context = context;
            }

            // GET: api/Customer
            [HttpGet]
            public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
            {
                var customers = await _context.Customers.ToListAsync();
                return Ok(customers);
            }
        }
    }
}
