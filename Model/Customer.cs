using System.ComponentModel.DataAnnotations;

namespace E_Banking_API.Model
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
      
        [StringLength(50)]
        public string FirstName { get; set; }
      
        [StringLength(50)]
        public string LastName { get; set; }
     
        [StringLength(100)]
        public string Email { get; set; }
      
        public string PasswordHash { get; set; }
    
        public string CustomerNumber { get; set; }
      
        public string CustomerPin { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
