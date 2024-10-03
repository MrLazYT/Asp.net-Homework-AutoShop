using DataAccess.Entities;

namespace AutoShop.Models
{
    public class OrderIncludeCars
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }

        public List<Car> Cars { get; set; }
    }
}
