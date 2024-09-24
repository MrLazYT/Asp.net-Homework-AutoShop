using DataAccess.Entities;

namespace AutoShop.Models
{
    public class ProductCartViewModel
    {
        public Car Car { get; set; } = default!;
        public bool IsInCart { get; set; } = default;
    }
}
