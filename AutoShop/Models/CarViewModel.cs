namespace DataAccess.Entities
{
    public class CarViewModel
    {
        public Car Car { get; set; } = default!;
        public List<Category> Categories { get; set; } = default!;

        public CarViewModel(List<Category> categories)
        {
            Categories = categories;
        }

        public CarViewModel(Car car, List<Category> categories)
        {
            Car = car;
            Categories = categories;
        }
    }
}
