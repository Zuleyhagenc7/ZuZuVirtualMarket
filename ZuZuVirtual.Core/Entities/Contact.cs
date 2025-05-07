using System.ComponentModel.DataAnnotations;
using ZuZuVirtual.Core.Entities;

namespace ZuZuVirtual.core.Entities
{
    public class Contact : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Name"), Required(ErrorMessage = "{0} this field can not be left blank!" )]
        public string Name { get; set; }
        [Display(Name = "Surname"), Required(ErrorMessage = "{0} this field can not be left blank!")]
        public string Surname { get; set; }
        [Display(Name = "Email")]
        public string? Email { get; set; }
        [Display(Name = "Phone")]
        public string? Phone { get; set; }
        [Display(Name = "Message"), Required(ErrorMessage = "{0} this field can not be left blank!")]
        public string Message { get; set; }
        [Display(Name = "Create Date"), ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}

//Used to store, authenticate and manage contact information