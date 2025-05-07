using ZuZuVirtual.Core.Entities;

namespace ZuZuVirtualMarket.WebUI.Models
{
    public class CartViewModel
    {
        public List <CartLine>? CartLines { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
