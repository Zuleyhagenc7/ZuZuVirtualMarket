using System.ComponentModel.DataAnnotations;
using ZuZuVirtual.Core.Entities;


//Used to store, authenticate and manage product information

namespace ZuZuVirtual.core.Entities
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string? Description { get; set; }
        [Display(Name = "Image")]
        public string? Image { get; set; }
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        [Display(Name = "Product Code")]
        public string? ProductCode { get; set; }
        [Display(Name = "Stock")]
        public int Stock { get; set; }
        [Display(Name = "Is Active?")]
        public bool IsActive { get; set; }
        [Display(Name = "Is Home?")]
        public bool IsHome { get; set; }
        [Display(Name = "Category Id")]
        public int? CategoryId { get; set; }
        [Display(Name = "Category")]
        public Category? Category { get; set; }
        [Display(Name = "Brand Id")]
        public int? BrandId { get; set; }
        [Display(Name = "Brand")]
        public Brand? Brand { get; set; }
        [Display(Name = "Order No")]
        public int OrderNo { get; set; }
        [Display(Name = "Create Date"), ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
