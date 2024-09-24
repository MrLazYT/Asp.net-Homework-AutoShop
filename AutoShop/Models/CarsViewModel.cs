using AutoShop.Models;

namespace DataAccess.Entities
{
    public class CarsViewModel
    {
        public List<ProductCartViewModel> Cars { get; set; }
        public List<string> CarsProperties { get; set; }
        public List<Category> Categories { get; set; }

        public CarsViewModel(List<ProductCartViewModel> cars, List<Category> categories)
        {
            Cars = cars;
            CarsProperties = new List<string>() { "Id", "Model", "Color", "Year", "CategoryId" };
            Categories = categories;
        }
    }
}