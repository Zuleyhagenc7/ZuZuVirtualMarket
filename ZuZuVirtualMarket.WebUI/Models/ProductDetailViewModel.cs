using ZuZuVirtual.core.Entities;

namespace ZuZuVirtualMarket.WebUI.Models
{
    public class ProductDetailViewModel
    {
        public Product? Product { get; set; }
        public IEnumerable<Product>? RelatedProducts { get; set; }
    }
}
