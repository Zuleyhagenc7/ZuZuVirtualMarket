using System.ComponentModel.DataAnnotations;

namespace ZuZuVirtualMarket.WebUI.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress), Required(ErrorMessage = "Email cannot be left blank!")]
        public string Email { get; set; }
        [Display(Name = "Password"), Required(ErrorMessage = "Password cannot be left blank!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
        public bool RememberMe { get; set; }

    }
}
