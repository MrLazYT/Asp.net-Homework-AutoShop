using AutoShop.Services;
using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace AutoShop.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        public readonly CarContext _context;
        public readonly SessionData _sessionData;

        public CartController(CarContext context, SessionData sessionData)
        {
            _context = context;
            _sessionData = sessionData;
        }

        public IActionResult Index()
        {
            Dictionary<int, int> idsAndQuantities = _sessionData.GetCartData();
            List<int> ids = idsAndQuantities.Keys.ToList();
            DbSet<Car> cars = _context.Cars;
            IIncludableQueryable<Car, Category> carsWithCat = cars.Include(car => car.Category)!;
            List<Car> carsInCart = null!;
            
            if (carsWithCat != null)
            {
                carsInCart = carsWithCat.Where(car => ids.Contains(car.Id)).ToList();
            }


            return View(carsInCart);
        }

        public IActionResult Add(int id)
        {
            Dictionary<int, int> idsAndQuantities = _sessionData.GetCartData();

            InvokeFunc(null!, id, controllerName: "Cars");

            return RedirectToAction("Index", "Cars");
        }

        public IActionResult Remove(int id)
        {
            InvokeFunc(RemoveCarIdFromCart, id);

            return RedirectToAction("Index");
        }

        public IActionResult PlusProductQuantity(int id)
        {
            InvokeFunc(PlusQuantity, id);

            return RedirectToAction("Index");
        }

        public IActionResult MinusProductQuantity(int id)
        {
            InvokeFunc(MinusQuantity, id);

            return RedirectToAction("Index");
        }

        private IActionResult InvokeFunc(Func<int, Dictionary<int, int>, Dictionary<int, int>> func, int id, string controllerName = "Cart")
        {
            Dictionary<int, int> idsAndQuantities = _sessionData.GetCartData();

            if (!idsAndQuantities.ContainsKey(id))
            {
                idsAndQuantities.Add(id, 1);
            }

            if (func != null)
            {
                idsAndQuantities = func.Invoke(id, idsAndQuantities);
            }

            _sessionData.SetCartData(idsAndQuantities);

            return RedirectToAction("Index", controllerName);
        }

        private Dictionary<int, int> RemoveCarIdFromCart(int id, Dictionary<int, int> idsAndQuantities)
        {
            idsAndQuantities.Remove(id);

            return idsAndQuantities;
        }

        private Dictionary<int, int> PlusQuantity(int id, Dictionary<int, int> idsAndQuantities)
        {
            idsAndQuantities[id] = ++idsAndQuantities[id];

            return idsAndQuantities;
        }

        private Dictionary<int, int> MinusQuantity(int id, Dictionary<int, int> idsAndQuantities)
        {
            if (idsAndQuantities[id] != 1)
            {
                idsAndQuantities[id] = --idsAndQuantities[id];
            }

            return idsAndQuantities;
        }
    }
}