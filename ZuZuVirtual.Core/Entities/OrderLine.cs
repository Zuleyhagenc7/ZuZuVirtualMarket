using System.ComponentModel.DataAnnotations;
using ZuZuVirtual.core.Entities;

namespace ZuZuVirtual.Core.Entities
{
    public class OrderLine : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Order Number")]
        public int OrderId { get; set; }
        [Display(Name = "Order")]
        public Order? Order { get; set; }
        [Display(Name = "Product Number")]
        public int ProductId { get; set; }
        [Display(Name = "Product")]
        public Product? Product { get; set; }
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }
    }
}
