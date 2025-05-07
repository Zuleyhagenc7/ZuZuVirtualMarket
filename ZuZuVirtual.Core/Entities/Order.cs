using System.ComponentModel.DataAnnotations;
using ZuZuVirtual.core.Entities;

namespace ZuZuVirtual.Core.Entities
{
    public class Order : IEntity
    {
        public int Id { get;  set; }
        [Display(Name ="Order Number"), StringLength(50)]
        public string OrderNumber { get;  set; }
        [Display(Name = "Total Price")]
        public decimal TotalPrice { get;  set; }
        [Display(Name = "Customer Number")]
        public int AppUserId { get;  set; }
        [Display(Name = "Costumer"), StringLength(50)]
        public string CustomerId  { get;  set; }
        [Display(Name = "Billing Address"), StringLength(50)]
        public string BillingAddress  { get;  set; }
        [Display(Name = "Delivery Address"), StringLength(50)]
        public string DeliveryAddress  { get;  set; }
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get;  set; }
        public List<OrderLine>? OrderLines { get; set; }
        [Display(Name = "Costumer")]
        public AppUser? AppUser { get; set; }
        [Display(Name = "Order Status")]
        public EnumOrderState OrderState { get; set; }
    }
    public enum EnumOrderState
    {
        [Display(Name = "Awaiting Approval...")]
        Waiting,
        [Display(Name = "Approved.")]
        Approved,
        [Display(Name = "In Cargo.")]
        Shipped,
        [Display(Name = "Delivered.")]
        Completed,
        [Display(Name = "Canceled.")]
        Cancelled,
        [Display(Name = "Returned.")]
        Returned
        
    }
}
