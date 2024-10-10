using AutoShop.Helpers;
using AutoShop.Models;
using BusinessLogic.Services;
using BusinessLogic.Validators;
using BusinessLogic.DTOs;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using BusinessLogic.Interfaces;

namespace AutoShop.Controllers
{
    public class CarsController : Controller
	{
        private readonly CarService _carService;
        private readonly CategoryService _categoryService;
        private readonly IFileService _fileService;
		private readonly CarViewModel _carViewModel;
		private readonly CarsViewModel _carsViewModel;
        private readonly CarModelValidator _validator;
        private readonly CarData _carData;

        public CarsController(SessionData sessionData, CarService carService, CategoryService categoryService, IFileService fileService)
		{
            _carData = new CarData(carService, sessionData);
            _carService = carService;
            _categoryService = categoryService;
            _fileService = fileService;

			List<ProductCartViewModel> productCartViewModels = _carData.GetProductCartViewModels();
            List<CategoryDto> categories = new List<CategoryDto>()
            {
                new CategoryDto {
                    Id = 0,
                    Name = "All",
                    Description = "Shows all the categories"
                }
            };

            categories.AddRange(_categoryService.GetAll());

            List<CarDto> topFiveCars = _carService.GetTopFive();

			_carsViewModel = new CarsViewModel(productCartViewModels, categories, topFiveCars);
			_carViewModel = new CarViewModel(new CarDto(), categories);
			_validator = new CarModelValidator(_carService, ModelState);
		}

        [HttpGet]
        public IActionResult Index(int propertyId, int categoryId, bool isDescending)
        {
            SetViewData(propertyId, categoryId, isDescending);

            PropertyInfo property = GetCarProperty(propertyId);
            _carsViewModel.Cars = GetFilteredCars(categoryId, isDescending, property);

            return View(_carsViewModel);
        }

        private void SetViewData(int propertyId, int categoryId, bool isDescending)
		{
            ViewBag.CarPropertyId = propertyId;
            ViewBag.SelectedCategoryId = categoryId;
			ViewBag.IsDescending = isDescending;
        }

		private PropertyInfo GetCarProperty(int propertyId)
		{
            string propertyName = _carsViewModel.CarsProperties[propertyId];
            PropertyInfo property = CarDto.GetPropertyInfo(propertyName);

			return property;
        }

		private List<ProductCartViewModel> GetFilteredCars(int categoryId, bool isDescending, PropertyInfo property)
		{
            List<CarDto> carsByCategory = _carService.GetAllByCategory(categoryId);
			List<CarDto> carsByOrder;

			if (!isDescending)
			{
				carsByOrder = _carService.GetAllSorted(carsByCategory, property);
			}
			else
			{
				carsByOrder = _carService.GetAllSortedDesc(carsByCategory, property);
			}

            List<ProductCartViewModel> productCartViewModel = _carData.GetProductCartViewModels(carsByOrder);

			return productCartViewModel;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View(_carViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(CarDto car)
        {
            return ExecuteActionIfCarExists(_carService.Add, car);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
		public IActionResult Edit(int id)
		{
            CarDto car = _carService.GetById(id);
            
			return ReloadView(car);
        }

		[HttpPost]
        [Authorize(Roles = "Admin")]
		public IActionResult Edit(int id, CarDto car)
		{
			car.Id = id;

			return ExecuteActionIfCarExists(_carService.Update, car);
		}

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            CarDto car = _carService.GetById(id);

            return ExecuteActionIfCarExists(_carService.Delete, car, true);
        }

        private IActionResult ExecuteActionIfCarExists(Action<CarDto> action, CarDto car, bool isDeleting = false)
        {
            if (car == null)
            {
                return NotFound();
            }

            return ValidateAndExecuteAction(action, car, isDeleting);
        }

        private IActionResult ValidateAndExecuteAction(Action<CarDto> action, CarDto car, bool isDeleting = false)
        {
            bool isCarValidRes = _validator.IsCarValid(car);

            if (isCarValidRes || isDeleting)
            {

                return ExecuteAction(action, car);
            }

            AddModelErrors(isCarValidRes);

            return ReloadView(car);
        }

        private IActionResult ExecuteAction(Action<CarDto> action, CarDto car)
		{
            action.Invoke(car);

            return RedirectToAction("Index");
        }

        private IActionResult ReloadView(CarDto car)
		{
            _carViewModel.Car = car;

            return View(_carViewModel);
        }

        private void AddModelErrors(bool areCarFieldsValid)
        {
            if (!areCarFieldsValid)
            {
                ModelState.AddModelError("All", "Fields must be filled correctly");
            }
        }
    }
}