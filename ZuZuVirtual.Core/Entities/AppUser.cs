using System.ComponentModel.DataAnnotations;
using ZuZuVirtual.Core.Entities;


namespace ZuZuVirtual.core.Entities
{
    public class AppUser : IEntity
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
        [Display(Name = "Username")]
        public string? UserName { get; set; }
        [Display(Name = "Is Active?")]
        public bool IsActive { get; set; }
        [Display(Name = "Is Admin?")]
        public bool IsAdmin { get; set; }
        [Display(Name = "Create Date"), ScaffoldColumn(false)] //This value is not avaliable on automatically generated pages
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [ScaffoldColumn(false)]
        public Guid? UserGuid { get; set; } = Guid.NewGuid();
        public List<Address>? Addresses {get; set;} 

    }
}
//Use to store, authenticate and manage user information

