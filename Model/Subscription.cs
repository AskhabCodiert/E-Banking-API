using System.ComponentModel.DataAnnotations;

namespace E_Banking_API.Model
{
    public class Subscription
    {
        [Key]
        [Required]
        public int SubscriptionID { get; set; }
        [Required]
        public int CustomerID { get; set; }
        [Required]
        public int AccountID { get; set; }
        [Required]
        [StringLength(100)]
        public string ServiceName { get; set; }
        [Required]
        public decimal MonthlyFee { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime? EndDate { get; set; }
        [Required]
        public bool Active { get; set; }
        
    }
}
