using DataAccess.Helpers;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query;
using System.Reflection;

namespace DataAccess.Data
{
	public class CarContext : DbContext
	{
        public DbSet<Car> Cars { get; set; }
        public DbSet<Category> Categories { get; set; }

        public CarContext(DbContextOptions<CarContext> options) : base(options)
		{
			Database.EnsureCreated();
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            EntityTypeBuilder<Category> categoryBuilder = modelBuilder.Entity<Category>();
            EntityTypeBuilder<Car> carBuilder = modelBuilder.Entity<Car>();

            CategoryInitializer.SeedData(categoryBuilder);
            CarInitializer.SeedData(carBuilder);
        }

        public void AddCar(Car car)
        {
            Cars.Add(car);
            SaveChanges();
        }

        public void UpdateCar(Car car)
        {
            Cars.Update(car);
            SaveChanges();
        }

        public void RemoveCar(Car car)
        {
            Cars.Remove(car);
            SaveChanges();
        }

        public Car GetCarById(int id)
        {
            List<Car> cars = GetCarList();
            Car car = cars.FirstOrDefault(car => car.Id == id)!;

            return car;
        }

        public List<Car> GetCarList()
		{
			IIncludableQueryable<Car, Category> cars = IncludeCategories(Cars);

			return cars.ToList();
		}

        public IIncludableQueryable<Car, Category> IncludeCategories(IQueryable<Car> cars)
        {
            return cars.Include(car => car.Category)!;
        }

        public List<Category> GetCategoryList()
        {
            return Categories.ToList();
        }

        public List<Car> GetCarListByCategory(int categoryId)
		{
			if (categoryId != 0)
			{
				IQueryable<Car> cars = Cars.Where(car => car.CategoryId == categoryId);

				return cars.ToList();
			}

			return Cars.ToList();
		}

        public List<Car> GetSortedCars(List<Car> cars, PropertyInfo property)
		{
			return SortCarsUsingFunc(SortCars, cars, property);
		}

		public List<Car> GetSortedCarsDesc(List<Car> cars, PropertyInfo property)
		{
			return SortCarsUsingFunc(SortCarsDesc, cars, property);
		}

        private List<Car> SortCarsUsingFunc(
            Func<List<Car>, PropertyInfo, IOrderedEnumerable<Car>> func,
            List<Car> cars,
            PropertyInfo property)
        {
            if (property != null)
            {
                IOrderedEnumerable<Car> orderedCars = func.Invoke(cars, property);

                return orderedCars.ToList();
            }

            return cars;
        }

        private IOrderedEnumerable<Car> SortCars(List<Car> cars, PropertyInfo property)
        {
            return cars.OrderBy(car => property.GetValue(car, null));
        }

        private IOrderedEnumerable<Car> SortCarsDesc(List<Car> cars, PropertyInfo property)
        {
            return cars.OrderByDescending(car => property.GetValue(car, null));
        }
    }
}