using AutoShop.Helpers;
using AutoShop.Models;
using BusinessLogic.DTOs;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AutoShop.Controllers
{
    public class HomeController : Controller
	{
		private readonly CarService _carService;
		private readonly SessionData _sessionData;
		private readonly CarData _carData;

		public HomeController(CarService carService, SessionData sessionData)
		{
			_carService = carService;
			_sessionData = sessionData;
			_carData = new CarData(_carService, _sessionData);
		}

		public IActionResult Index(int? pageIndex, string? sortOrder)
		{
			int pageSize = 3;

			ViewData["CurrentSort"] = sortOrder;
			ViewData["IdSort"] = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
			ViewData["ModelSort"] = sortOrder == "model" ? "model_desc" : "model";
			ViewData["ColorSort"] = sortOrder == "color" ? "color_desc" : "color";
			ViewData["YearSort"] = sortOrder == "year" ? "year_desc" : "year";
			ViewData["CategorySort"] = sortOrder == "category" ? "category_desc" : "category";
			ViewData["PriceSort"] = sortOrder == "price" ? "price_desc" : "price";
			
			List<ProductCartViewModel> productCartViewModels = _carData.GetProductCartViewModels();
            List<CarDto> cars = _carData.GetCars(productCartViewModels);

            switch (sortOrder)
			{
				case "id_desc":
                    productCartViewModels = _carData.GetProductCartViewModels(cars.OrderByDescending(c => c.Id));
                    break;
				case "model":
                    productCartViewModels = _carData.GetProductCartViewModels(cars.OrderBy(c => c.Model));
                    break;
				case "model_desc":
                    productCartViewModels = _carData.GetProductCartViewModels(cars.OrderByDescending(c => c.Model));
                    break;
				case "color":
                    productCartViewModels = _carData.GetProductCartViewModels(cars.OrderBy(c => c.Color));
                    break;
                case "color_desc":
                    productCartViewModels = _carData.GetProductCartViewModels(cars.OrderByDescending(c => c.Color));
                    break;
                case "year":
                    productCartViewModels = _carData.GetProductCartViewModels(cars.OrderBy(c => c.Year));
                    break;
                case "year_desc":
                    productCartViewModels = _carData.GetProductCartViewModels(cars.OrderByDescending(c => c.Year));
                    break;
                case "category":
                    productCartViewModels = _carData.GetProductCartViewModels(cars.OrderBy(c => c.CategoryName));
                    break;
				case "category_desc":
                    productCartViewModels = _carData.GetProductCartViewModels(cars.OrderByDescending(c => c.CategoryName));
                    break;
                case "price":
                    productCartViewModels = _carData.GetProductCartViewModels(cars.OrderBy(c => c.Price));
                    break;
                case "price_desc":
                    productCartViewModels = _carData.GetProductCartViewModels(cars.OrderByDescending(c => c.Price));
                    break;
                default:
                    productCartViewModels = _carData.GetProductCartViewModels(cars.OrderBy(c => c.Id));
                    break;
            }

			PaginatedList<ProductCartViewModel> paginatedList = PaginatedList<ProductCartViewModel>.Create(productCartViewModels.AsQueryable(), pageIndex ?? 1, pageSize);


			return View(paginatedList);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		private bool IsProductInCart(int id)
		{
			Dictionary<int, int> idsAndQuantities = _sessionData.GetCartData();

			if (idsAndQuantities != null)
			{
				return false;
			}

			return idsAndQuantities!.ContainsKey(id);
		}
	}
}
