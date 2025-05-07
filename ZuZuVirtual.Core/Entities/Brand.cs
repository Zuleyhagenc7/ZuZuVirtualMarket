using System.ComponentModel.DataAnnotations;
using ZuZuVirtual.Core.Entities;

namespace ZuZuVirtual.core.Entities
{
    public class Brand : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string? Description { get; set; }
        [Display(Name = "Logo")]
        public string? Logo { get; set; }
        [Display(Name = "Is Active?")]
        public bool IsActive { get; set; }
        [Display(Name = "Order No")]
        public int OrderNo { get; set; }
        [Display(Name = "Create Date"), ScaffoldColumn(false)] //Controls whether to display or not
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public IList<Product>? Products { get; set; }
    }
}

//Used to store, authenticate and manage brand information