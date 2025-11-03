using System.ComponentModel.DataAnnotations;

namespace Temiang.Avicenna.Model
{
    public class UserLoginModel
    {
        [Required]
        [Display(Name = "User ID")]
        public string UserID { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}