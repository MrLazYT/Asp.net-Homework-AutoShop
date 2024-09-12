namespace DataAccess.Models
{
    public class CarsViewModel
    {
        public List<Car> Cars { get; set; }
        public List<string> CarsProperties { get; set; }
        public List<Category> Categories { get; set; }

        public CarsViewModel(List<Car> cars, List<Category> categories)
        {
            Cars = cars;
            CarsProperties = new List<string>() { "Id", "Model", "Color", "Year", "CategoryId" };
            Categories = categories;
        }
    }
}