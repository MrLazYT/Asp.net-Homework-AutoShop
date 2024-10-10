using BusinessLogic.DTOs;

namespace DataAccess.Entities
{
    public class CarViewModel
    {
        public CarDto Car { get; set; } = default!;
        public List<CategoryDto> Categories { get; set; } = default!;

        public CarViewModel(List<CategoryDto> categories)
        {
            Categories = categories;
        }

        public CarViewModel(CarDto car, List<CategoryDto> categories)
        {
            Car = car;
            Categories = categories;
        }
    }
}
