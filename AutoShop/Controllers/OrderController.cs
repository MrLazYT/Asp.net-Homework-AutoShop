using AutoShop.Models;
using BusinessLogic.Services;
using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AutoShop.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly CarContext _context;
        private readonly SessionData _sessionData;

        public OrderController(CarContext context, SessionData sessionData)
        {
            _context = context;
            _sessionData = sessionData;
        }

        public IActionResult Index()
        {
            List<OrderIncludeCars> ordersIncludeCars = GetOrdersIncludeCars();

            return View(ordersIncludeCars);
        }

        private List<OrderIncludeCars> GetOrdersIncludeCars()
        {
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            List<Order> orders = _context.GetOrderListByUserId(userId);
            List<Car> cars = _context.GetCarList();
            
            return orders.Select(order => GetOrderIncludeCars(order)).ToList();
        }

        private OrderIncludeCars GetOrderIncludeCars(Order order)
        {
            string ids = order.IdsProduct;
            List<Car> selectedCars = _context.GetCarsFromIdsString(ids);

            return new OrderIncludeCars()
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                Cars = selectedCars
            };
        }

        public IActionResult Create()
        {
            string? userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            Order order = CreateOrder();
            
            RemoveCarsFromStorage();
            UpdateSoldCarsCount();
            
            _sessionData.ClearData();
            _context.AddOrder(order);

            
            return RedirectToAction("Index");
        }

        private Order CreateOrder()
        {
            string? userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<Car> cars = _context.GetCarList();
            Dictionary<int, int> idsAndQuantities = _sessionData.GetCartData();
            string carIdsString = IdsAndQuantitiesToString(idsAndQuantities);
            List<Car> selectedCars = _context.GetCarsFromIdsString(carIdsString);

            return new Order()
            {
                OrderDate = DateTime.Now,
                TotalPrice = selectedCars.Sum(car => car.Price),
                IdsProduct = carIdsString,
                UserId = userId,
            };
        }

        private string IdsAndQuantitiesToString(Dictionary<int, int> idsAndQuantities)
        {
            if (idsAndQuantities == null)
            {
                return "";
            }

            List<string> carIdStrings = idsAndQuantities.Select(idAndQuantity => IdAndQuantityToString(idAndQuantity)).ToList();

            string ids = String.Join(", ", carIdStrings);

            return ids;
        }

        private string IdAndQuantityToString(KeyValuePair<int, int> idAndQuantity)
        {
            string prompt = $"{idAndQuantity.Key}";
            string res = prompt;

            for (int i = 1; i < idAndQuantity.Value; i++)
            {
                res += ", ";
                res += idAndQuantity.Key;
            }

            return res;
        }

        private void RemoveCarsFromStorage()
        {
            List<Car> cars = _context.GetCarList();
            Dictionary<int, int> idsAndQuantities = _sessionData.GetCartData();
            string carIdsString = IdsAndQuantitiesToString(idsAndQuantities);
            List<Car> selectedCars = _context.GetCarsFromIdsString(carIdsString);
            List<StorageItem> storageItems = _context.StorageItems.ToList();

            foreach (var car in selectedCars)
            {
                StorageItem? storageItem = storageItems.FirstOrDefault(storageItem => storageItem.CarId == car.Id);

                if (storageItem != null)
                {
                    if (_context.Entry(storageItem).State == EntityState.Unchanged)
                    {
                        _context.Entry(storageItem).State = EntityState.Detached;
                    }

                    storageItem.Count -= 1;

                    storageItems[storageItem.Id - 1] = storageItem;
                }
            }

            foreach (var storageItem in storageItems)
            {
                _context.Update(storageItem);
                _context.SaveChanges();
            }
        }

        private void UpdateSoldCarsCount()
        {
            List<Car> cars = _context.GetCarList();
            Dictionary<int, int> idsAndQuantities = _sessionData.GetCartData();
            string carIdsString = IdsAndQuantitiesToString(idsAndQuantities);
            List<Car> selectedCars = _context.GetCarsFromIdsString(carIdsString);

            foreach (var car in selectedCars)
            {
                car.SoldCount++;

                _context.Update(car);
                _context.SaveChanges();
            }
        }
    }
}