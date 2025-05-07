using System.ComponentModel.DataAnnotations;
using ZuZuVirtual.Core.Entities;

namespace ZuZuVirtual.core.Entities
{
    public class Category : IEntity
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
        [Display(Name = "Is Top Menü?")]
        public bool IsTopMenu { get; set; }
        [Display(Name = "Top Menü")]
        public int ParentId { get; set; }
        [Display(Name = "Order No")]
        public int OrderNo { get; set; }
        [Display(Name = "Create Date"), ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public IList<Product>? Products { get; set; }
    }
}

//Used to store, authenticate and manage category information
