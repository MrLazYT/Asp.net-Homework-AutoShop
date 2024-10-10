using AutoShop.Models;
using BusinessLogic.DTOs;

namespace DataAccess.Entities
{
    public class CarsViewModel
    {
        public List<ProductCartViewModel> Cars { get; set; }
        public List<string> CarsProperties { get; set; }
        public List<CategoryDto> Categories { get; set; }
        public List<CarDto> TopFive { get; set; }

        public CarsViewModel(List<ProductCartViewModel> cars, List<CategoryDto> categories, List<CarDto> topFive)
        {
            Cars = cars;
            CarsProperties = new List<string>() { "Id", "Model", "Color", "Year", "CategoryName", "Price" };
            Categories = categories;
            TopFive = topFive;
        }
    }
}