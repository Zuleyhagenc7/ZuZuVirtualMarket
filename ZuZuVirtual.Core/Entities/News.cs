using System.ComponentModel.DataAnnotations;
using ZuZuVirtual.Core.Entities;

namespace ZuZuVirtual.core.Entities
{
    public class News : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string? Description { get; set; }
        [Display(Name = "Image")]
        public string? Image { get; set; }
        [Display(Name = "Is Active?")]
        public bool IsActive { get; set; }
        [Display(Name = "Create Date"), ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; } = DateTime.Now;

    }
}

//Used to store, authenticate and manage news information