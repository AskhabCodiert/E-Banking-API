using System.ComponentModel.DataAnnotations;

namespace E_Banking_API.Model
{
    public class Transaction
    {
        [Key]
        [Required]
        public int TransactionID { get; set; }
        [Required]
        public int AccountID { get; set; }
        [Required]
        public int TargetAccountID { get; set; }
        [Required]
        public string TransactionType { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public DateTime TransactionDate { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
