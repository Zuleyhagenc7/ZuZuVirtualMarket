using System.ComponentModel.DataAnnotations;

namespace ZuZuVirtualMarket.WebUI.Models
{
    public class UserEditViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Surname")]
        public string SurName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Phone")]
        public string? Phone { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
