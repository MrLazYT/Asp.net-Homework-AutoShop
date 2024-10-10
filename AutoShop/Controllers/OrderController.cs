using BusinessLogic.DTOs;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoShop.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            List<OrderDto> orders = _orderService.GetAll();

            return View(orders);
        }

        public async Task<IActionResult> Create()
        {
            await _orderService.Add();
            
            return RedirectToAction("Index");
        }
    }
}