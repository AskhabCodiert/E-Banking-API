using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Banking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ALL
        [HttpGet]
        public  IActionResult GetAllCustomers()
        {
            var customers = _context.Customers.ToList();
            return Ok(customers);
        }

        // GET BY ID
        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.CustomerID  == id);

            if (customer == null)
            {

                return NotFound();
            }

            return Ok(customer);
        }

     
    }
}
