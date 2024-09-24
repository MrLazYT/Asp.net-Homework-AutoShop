using AutoShop.Helpers;
using AutoShop.Models;
using AutoShop.Services;
using AutoShop.Validators;
using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace AutoShop.Controllers
{
    public class CarsController : Controller
	{
		public readonly CarContext _context;
		public readonly CarViewModel _carViewModel;
		public readonly CarsViewModel _carsViewModel;
        public readonly CarModelValidator _validator;
        private readonly CarData _carData;

        public CarsController(CarContext context, SessionData sessionData)
		{
			_context = context;
            _carData = new CarData(_context, sessionData);

            List<Car> cars = _context.GetCarList();
			List<ProductCartViewModel> productCartViewModels = _carData.GetProductCartViewModels();
            List<Category> categories = new List<Category>()
            {
                new Category {
                    Id = 0,
                    Name = "All",
                    Description = "Shows all the categories"
                }
            };

            categories.AddRange(_context.GetCategoryList());

			_carsViewModel = new CarsViewModel(productCartViewModels, categories);
			_carViewModel = new CarViewModel(new Car(), categories);
			_validator = new CarModelValidator(_context, ModelState);
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
            PropertyInfo property = Car.GetPropertyInfo(propertyName);

			return property;
        }

		private List<ProductCartViewModel> GetFilteredCars(int categoryId, bool isDescending, PropertyInfo property)
		{
            List<Car> carsByCategory = _context.GetCarListByCategory(categoryId);
			List<Car> carsByOrder;

			if (!isDescending)
			{
				carsByOrder = _context.GetSortedCars(carsByCategory, property);
			}
			else
			{
				carsByOrder = _context.GetSortedCarsDesc(carsByCategory, property);
			}

            List<ProductCartViewModel> productCartViewModel = _carData.GetProductCartViewModels(carsByOrder);

			return productCartViewModel;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(_carViewModel);
        }

        [HttpPost]
        public IActionResult Add(Car car)
        {
            return ExecuteActionIfCarExists(_context.AddCar, car);
        }

        [HttpGet]
		public IActionResult Edit(int id)
		{
			Car car = _context.GetCarById(id);
            
			return ReloadView(car);
        }

		[HttpPost]
		public IActionResult Edit(int id, Car car)
		{
			car.Id = id;

			return ExecuteActionIfCarExists(_context.UpdateCar, car);
		}

        public IActionResult Delete(int id)
        {
            Car car = _context.GetCarById(id);

            return ExecuteActionIfCarExists(_context.RemoveCar, car, true);
        }

        private IActionResult ExecuteActionIfCarExists(Action<Car> action, Car car, bool isDeleting = false)
        {
            if (car == null)
            {
                return NotFound();
            }

            return ValidateAndExecuteAction(action, car, isDeleting);
        }

        private IActionResult ValidateAndExecuteAction(Action<Car> action, Car car, bool isDeleting = false)
        {
            bool[] isCarValidRes = _validator.IsCarValid(car);

            if (isCarValidRes[0] && isCarValidRes[1] || isDeleting)
            {
                return ExecuteAction(action, car);
            }

            AddModelErrors(isCarValidRes[0], isCarValidRes[1]);

            return ReloadView(car);
        }

        private IActionResult ExecuteAction(Action<Car> action, Car car)
		{
            action.Invoke(car);

            return RedirectToAction("Index");
        }

		private IActionResult ReloadView(Car car)
		{
            _carViewModel.Car = car;

            return View(_carViewModel);
        }

        private void AddModelErrors(bool areCarFieldsValid, bool areFieldsChanged)
        {
            if (!areCarFieldsValid)
            {
                ModelState.AddModelError("All", "Fields must be filled correctly");
            }

            if (!areFieldsChanged)
            {
                ModelState.AddModelError("All", "Fields cannot be the same");
            }
        }
    }
}