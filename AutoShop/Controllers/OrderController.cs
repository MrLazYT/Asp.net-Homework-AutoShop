using AutoShop.Models;
using AutoShop.Services;
using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    }
}