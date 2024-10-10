using AutoShop.Models;
using BusinessLogic.DTOs;
using BusinessLogic.Services;

namespace AutoShop.Helpers
{
    public class CarData
    {
        private readonly CarService _carService;
        private readonly SessionData _sessionData;

        public CarData(CarService carService, SessionData sessionData)
        {
            _carService = carService;
            _sessionData = sessionData;
        }

        public List<ProductCartViewModel> GetProductCartViewModels()
        {
            List<CarDto> cars = _carService.GetAll();
            List<ProductCartViewModel> productCartViewModels = GetProductCartViewModels(cars);

            return productCartViewModels;
        }

        public List<ProductCartViewModel> GetProductCartViewModels(IEnumerable<CarDto> cars)
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

        public List<CarDto> GetCars(List<ProductCartViewModel> productCartViewModels)
        {
            IEnumerable<CarDto> cars = productCartViewModels.Select(viewModel => new CarDto()
            {
                Id = viewModel.Car.Id,
                ImagePath = viewModel.Car.ImagePath,
                Model = viewModel.Car.Model,
                Color = viewModel.Car.Color,
                Year = viewModel.Car.Year,
                CategoryId = viewModel.Car.CategoryId,
                CategoryName = viewModel.Car.CategoryName,
                Price = viewModel.Car.Price,
            });

            return cars.ToList();
        }
    }
}
