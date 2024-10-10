using BusinessLogic.Services;
using BusinessLogic.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Entities;

namespace AutoShop.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            List<CarDto> cars = _cartService.GetAllFromCart();

            return View(cars);
        }

        public IActionResult Add(int id)
        {
            CarDto carDto = _cartService.GetById(id);

            _cartService.Add(carDto);

            return RedirectToAction("Index", "Cars");
        }

        public IActionResult Remove(int id)
        {
            CarDto carDto = _cartService.GetById(id);

            _cartService.Delete(carDto);

            return RedirectToAction("Index");
        }

        public IActionResult PlusProductQuantity(int id)
        {
            _cartService.PlusQuantity(id);

            return RedirectToAction("Index");
        }

        public IActionResult MinusProductQuantity(int id)
        {
            _cartService.MinusQuantity(id);

            return RedirectToAction("Index");
        }
    }
}