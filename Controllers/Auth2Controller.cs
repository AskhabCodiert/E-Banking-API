using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using E_Banking_API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace E_Banking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Auth2Controller : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public Auth2Controller(ApplicationDbContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.CustomerNumber == request.CustomerNumber);

            if (customer == null || !BCrypt.Net.BCrypt.Verify(request.Password, customer.PasswordHash))
            {
                return Unauthorized(new LoginResponse
                {
                    IsSuccess = false,
                    ErrorMessage = "Invalid credentials."
                });
            }

            var token = GenerateJwtToken(customer);

            return Ok(new LoginResponse
            {
                IsSuccess = true,
                Token = token
            });
        }


        private string GenerateJwtToken(Customer customer)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, customer.CustomerNumber),

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims:  claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
