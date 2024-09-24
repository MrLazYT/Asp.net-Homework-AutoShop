using AutoShop.Models;
using AutoShop.Services;
using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace AutoShop.Helpers
{
    public class CarData
    {
        private readonly CarContext _context;
        private readonly SessionData _sessionData;

        public CarData(CarContext context, SessionData sessionData)
        {
            _context = context;
            _sessionData = sessionData;
        }

        public List<ProductCartViewModel> GetProductCartViewModels()
        {
            DbSet<Car> cars = _context.Cars;
            IIncludableQueryable<Car, Category> carsWithCat = cars.Include(c => c.Category)!;

            List<ProductCartViewModel> productCartViewModels = GetProductCartViewModels(carsWithCat);

            return productCartViewModels;
        }

        public List<ProductCartViewModel> GetProductCartViewModels(IEnumerable<Car> cars)
        {
            IEnumerable<ProductCartViewModel> productCartViewModels = cars.Select(c => new ProductCartViewModel()
            {
                Car = c,
                IsInCart = IsProductInCart(c.Id)
            });

            return productCartViewModels.ToList();
        }
        
        private bool IsProductInCart(int id)
        {
            Dictionary<int, int> idsAndQuantities = _sessionData.GetCartData();

            if (idsAndQuantities == null)
            {
                return false;
            }

            return idsAndQuantities!.ContainsKey(id);
        }

        public List<Car> GetCars(List<ProductCartViewModel> productCartViewModels)
        {
            IEnumerable<Car> cars = productCartViewModels.Select(viewModel => new Car()
            {
                Id = viewModel.Car.Id,
                ImagePath = viewModel.Car.ImagePath,
                Model = viewModel.Car.Model,
                Color = viewModel.Car.Color,
                Year = viewModel.Car.Year,
                CategoryId = viewModel.Car.CategoryId,
                Category = viewModel.Car.Category,
                Price = viewModel.Car.Price,
            });

            return cars.ToList();
        }
    }
}
