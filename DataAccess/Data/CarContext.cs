﻿using DataAccess.EntityInitializers;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataAccess.Data
{
	public class CarContext : IdentityDbContext
	{
        public DbSet<Car> Cars { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }

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

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarContext).Assembly);
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

        public void AddOrder(Order order)
        {
            Orders.Add(order);
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

        public List<Car> GetCarsFromIdsString(string carIdsString)
        {
            List<Car> cars = GetCarList();

            string formattedCarIds = carIdsString
                                        .Replace("[", "")
                                        .Replace("]", "");

            List<string> carIdsStrings = formattedCarIds.Split(',').ToList();
            List<int> carIds = carIdsStrings.Select(id => int.Parse(id)).ToList();
            List<Car> selectedCars = carIds.SelectMany(id => cars.Where(car => car.Id == id).Take(1)).ToList();

            return selectedCars;
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

        public List<Order> GetOrderListByUserId(string userId)
        {
            IQueryable<Order> orders = Orders.Where(order => order.UserId.Equals(userId));
            
            return orders.ToList();
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