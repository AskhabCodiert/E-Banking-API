using System.ComponentModel.DataAnnotations;

namespace E_Banking_API.Model
{
    public class Setting
    {
        [Key]
        public int SettingID { get; set; }
        [Required]
        public int CustomerID { get; set; }
        [Required]
        public string NotificationPreferences { get; set; }
        public string PrivacySettings { get; set; }
    }
}
