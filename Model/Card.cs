using System.ComponentModel.DataAnnotations;

namespace E_Banking_API.Model
{
    public class Card
    {
        [Key]
        public int CardID { get; set; }
        public int AccountID { get; set; }
        [Required]
        [StringLength(16)]
        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public DateTime ExpiryDate { get; set; }
        [Required]
        [StringLength(3)]
        public string CVV { get; set; }
        [Required]
        [StringLength(100)]
        public string CardHolderName { get; set; }
        public decimal MonthlyLimit { get; set; }
        public string Status { get; set; }
    }
}
