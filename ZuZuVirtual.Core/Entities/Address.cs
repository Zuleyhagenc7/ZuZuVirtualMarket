using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection.PortableExecutable;
using ZuZuVirtual.core.Entities;

namespace ZuZuVirtual.Core.Entities
{
    public class Address : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Address Header"), StringLength(50), Required(ErrorMessage ="{0} Field is required!")]
        public string Title { get; set; }
        [Display(Name = "City"), StringLength(50), Required(ErrorMessage = "{0} Field is required!")]
        public string City { get; set; }
        [Display(Name = "District"), StringLength(50), Required(ErrorMessage = "{0} Field is required!")]
        public string District { get; set; }
        [Display(Name = "Street Address"), DataType(DataType.MultilineText), Required(ErrorMessage = "{0} Field is required!")]
        public string OpenAddress { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        [Display(Name = "Billing Address")]
        public bool IsBillingAddress { get; set; }
        [Display(Name = "Delivery Address")]
        public bool IsDeliveryAddress { get; set; }
        [Display(Name = "Create Date"), ScaffoldColumn(false)] //This value is not avaliable on automatically generated pages
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [ScaffoldColumn(false)]
        public Guid? AddressGuid { get; set; } = Guid.NewGuid();
        public int? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
