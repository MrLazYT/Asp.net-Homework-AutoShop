using BusinessLogic.DTOs;

namespace AutoShop.Models
{
    public class ProductCartViewModel
    {
        public CarDto Car { get; set; } = default!;
        public bool IsInCart { get; set; } = default;
    }
}
