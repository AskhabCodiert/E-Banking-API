using System.ComponentModel.DataAnnotations;

namespace E_Banking_API.Model
{
    public class Account
    {
        [Key]
        public int AccountID { get; set; }
        [Required]
        public int CustomerID { get; set; }
        [Required]
        public AccountTypeEnum AccountType { get; set; }
        [Required]
        public double Balance { get; set; }
    }

    public enum AccountTypeEnum
    {
        Girokonto,
        Sparbuch,
        Tagesgeldkonto
    }
}
