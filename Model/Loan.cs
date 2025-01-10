using System.ComponentModel.DataAnnotations;

namespace E_Banking_API.Model
{
    public class Loan
    {
        [Key]
        public int LoanID { get; set; }
        [Required]
        public int CustomerID { get; set; }
        [Required]
        public decimal LoanAmount { get; set; }
        [Required]
        public decimal InterestRate { get; set; }
        [Required]
        public int LoanTermMonths { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public string Status { get; set; }

    }
}
