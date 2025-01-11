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
            var customer2 = _context.Customers.FirstOrDefault(c2 => c2.CustomerPin == request.CustomerPin);
            if (customer == null || customer2 == null)
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

        [HttpPost("validate")]
        public IActionResult ValidateToken()
        {
            // Holen Sie den Token aus dem Authorization-Header
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            if (authHeader == null || !authHeader.StartsWith("Bearer "))
            {
                return Unauthorized(new { message = "Token missing or invalid" });
            }

            var token = authHeader.Substring("Bearer ".Length);

            try
            {
                // Token validieren
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero // Optionale Toleranz für Zeitabweichungen
                };

                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                // Token ist gültig
                return Ok(new { message = "Token is valid" });
            }
            catch (Exception ex)
            {
                // Fehlerhafte Token-Verarbeitung
                return Unauthorized(new { message = "Token is invalid", error = ex.Message });
            }
        }

        private string GenerateJwtToken(Customer customer)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, customer.CustomerID.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
