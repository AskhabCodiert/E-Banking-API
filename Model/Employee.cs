using System.ComponentModel.DataAnnotations;

namespace E_Banking_API.Model
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        [StringLength(50)]
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
    }
}
