using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Entities;
using MailKit.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Security.Claims;

namespace BusinessLogic.Services
{
    public class OrderService
    {
        private readonly CarContext _context;
        private readonly HttpContext _httpContext;
        private readonly SessionData _sessionData;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;

        public OrderService(CarContext context, IHttpContextAccessor httpContextAccessor, SessionData sessionData, IMapper mapper, IMailService mailService)
        {
            _context = context;
            _httpContext = httpContextAccessor.HttpContext!;
            _mapper = mapper;
            _sessionData = sessionData;
            _mailService = mailService;
        }

        public async Task Add()
        {
            string? userId = _httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            Order order = CreateOrder();

            RemoveCarsFromStorage();
            UpdateSoldCarsCount();

            _sessionData.ClearData();

            _context.Add(order);
            _context.SaveChanges();

            await SendMailAsync();
        }

        private Order CreateOrder()
        {
            string? userId = _httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<CarDto> cars = GetCarList();
            Dictionary<int, int> idsAndQuantities = _sessionData.GetCartData();
            string carIdsString = IdsAndQuantitiesToString(idsAndQuantities);
            List<CarDto> selectedCars = GetCarsFromIdsString(carIdsString);

            return new Order()
            {
                OrderDate = DateTime.Now,
                TotalPrice = selectedCars.Sum(car => car.Price),
                IdsProduct = carIdsString,
                UserId = userId,
            };
        }

        private void RemoveCarsFromStorage()
        {
            Dictionary<int, int> idsAndQuantities = _sessionData.GetCartData();
            string carIdsString = IdsAndQuantitiesToString(idsAndQuantities);
            List<CarDto> selectedCars = GetCarsFromIdsString(carIdsString);
            List<StorageItem> storageItems = _context.StorageItems.ToList();

            foreach (var car in selectedCars)
            {
                RemoveCarFromStorage(car, storageItems);
            }

            foreach (var storageItem in storageItems)
            {
                _context.Update(storageItem);
                _context.SaveChanges();
            }
        }

        private void RemoveCarFromStorage(CarDto car, List<StorageItem> storageItems)
        {
            StorageItem? storageItem = storageItems.FirstOrDefault(storageItem => storageItem.CarId == car.Id);

            if (storageItem != null)
            {
                storageItem.Count -= 1;

                storageItems[storageItem.Id - 1] = storageItem;
            }
        }

        private void UpdateSoldCarsCount()
        {
            Dictionary<int, int> idsAndQuantities = _sessionData.GetCartData();
            string carIdsString = IdsAndQuantitiesToString(idsAndQuantities);
            List<CarDto> selectedCars = _context.Cars
                .Where(c => idsAndQuantities.Keys.Contains(c.Id))
                .AsNoTracking()
                .Select(c => _mapper.Map<CarDto>(c))
                .ToList();

            foreach (var car in selectedCars)
            {
                UpdateSoldCarCount(car);
            }
        }

        private void UpdateSoldCarCount(CarDto carDto)
        {
            Car? car = _context.Cars.FirstOrDefault(c => c.Id == carDto.Id);

            if (car != null)
            {
                car.SoldCount++;

                _context.Update(car);
                _context.SaveChanges();
            }
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

        private async Task SendMailAsync()
        {
            string? userId = _httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            string? userName = _httpContext.User.FindFirstValue(ClaimTypes.Name);
            Order order = _context.Orders.OrderBy(order => order.Id).Last();
            string text = $"<table class=\"wrapper-table\" cellpadding=\"5\" cellspacing=\"0\" width=\"100%\" border=\"0\" style=\"border-collapse:collapse; font-size:14px; line-height:1.5; background-repeat:no-repeat; background-position:top left\" background=\"https://s8859648.sendpul.se/image/747991a0e145ac2bbe69f063a9402e69/files/emailservice/userfiles/e87c2cc59bdd62b30b9245c0a00c5c32366067/abandoned_cart/bg-ab-3.png\">\r\n  <tbody>\r\n    <tr>\r\n      <td align=\"center\">\r\n        <table cellpadding=\"0\" cellspacing=\"0\" width=\"600px\" border=\"0\" bgcolor=\"transparent\" style=\"font-size:14px; line-height:1.5;\">\r\n          <tbody>\r\n            <tr>\r\n              <td>\r\n                <table cellpadding=\"0\" cellspacing=\"0\" style=\"width:100%\" border=\"0\">\r\n                  <tbody>\r\n                    <tr>\r\n                      <td style=\"padding:0; vertical-align:top\">\r\n                        <table cellpadding=\"0\" cellspacing=\"0\" style=\"width:100%\" border=\"0\">\r\n                          <tbody>\r\n                            <tr>\r\n                              <th style=\"text-align:left; font-weight:400; vertical-align:top\" align=\"left\">\r\n                                <table border=\"0\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">\r\n                                  <tbody>\r\n                                    <tr>\r\n                                      <td style=\"vertical-align:top\">\r\n                                        <table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"font-size:14px; line-height:1.5;\">\r\n                                          <tbody>\r\n                                            <tr class=\"content-row\">\r\n                                              <td style=\"padding:15px; vertical-align:top;\">\r\n                                                <p style=\"font-size:20px; font-weight:normal;\"><b>AutoShop</b></p>\r\n                                              </td>\r\n                                            </tr>\r\n                                          </tbody>\r\n                                        </table>\r\n                                      </td>\r\n                                    </tr>\r\n                                  </tbody>\r\n                                </table>\r\n                              </th>\r\n                            </tr>\r\n                          </tbody>\r\n                        </table>\r\n                      </td>\r\n                    </tr>\r\n                  </tbody>\r\n                </table>\r\n\r\n                <table cellpadding=\"0\" cellspacing=\"0\" style=\"width:100%\" border=\"0\">\r\n                  <tbody>\r\n                    <tr>\r\n                      <td style=\"padding:0; vertical-align:top\">\r\n                        <table cellpadding=\"0\" cellspacing=\"0\" style=\"width:100%\" border=\"0\">\r\n                          <tbody>\r\n                            <tr>\r\n                              <th style=\"text-align:left; font-weight:400; vertical-align:top\" align=\"left\">\r\n                                <table border=\"0\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">\r\n                                  <tbody>\r\n                                    <tr>\r\n                                      <td style=\"vertical-align:top\">\r\n                                        <table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"font-size:14px; line-height:1.5;\">\r\n                                          <tbody>\r\n                                            <tr class=\"content-row\">\r\n                                              <td style=\"padding:15px; vertical-align:top; color:#032575;\">\r\n                                                <p style=\"font-size:20px; font-weight:bolder; text-align:center;\">\r\n                                                  ДЯКУЄМО ЗА ВАШЕ ЗАМОВЛЕННЯ!\r\n                                                </p>\r\n                                              </td>\r\n                                            </tr>\r\n                                          </tbody>\r\n                                        </table>\r\n                                      </td>\r\n                                    </tr>\r\n                                  </tbody>\r\n                                </table>\r\n                              </th>\r\n                            </tr>\r\n                          </tbody>\r\n                        </table>\r\n                      </td>\r\n                    </tr>\r\n                  </tbody>\r\n                </table>\r\n\r\n                <table cellpadding=\"0\" cellspacing=\"0\" style=\"width:100%\" border=\"0\">\r\n                  <tbody>\r\n                    <tr>\r\n                      <td style=\"padding:0; vertical-align:top\">\r\n                        <table cellpadding=\"0\" cellspacing=\"0\" style=\"width:100%\" border=\"0\">\r\n                          <tbody>\r\n                            <tr>\r\n                              <th style=\"text-align:left; font-weight:400; vertical-align:top\" align=\"left\">\r\n                                <table border=\"0\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">\r\n                                  <tbody>\r\n                                    <tr>\r\n                                      <td style=\"vertical-align:top\">\r\n                                        <table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"background-color:#f5f7ff; font-size:14px; line-height:1.5;\">\r\n                                          <tbody>\r\n                                            <tr class=\"content-row\">\r\n                                              <td style=\"padding:30px 15px 1px; vertical-align:top; color:#032575;\">\r\n                                                <p style=\"font-size:20px; font-weight:bolder; text-align:center;\">\r\n                                                  ЗАМОВЛЕННЯ № {order.Id}\r\n                                                </p>\r\n                                              </td>\r\n                                            </tr>\r\n                                            <tr class=\"content-row\">\r\n                                              <td style=\"padding:15px; vertical-align:top; color:#778ff9;\">\r\n                                                <p style=\"font-size:16px;\">\r\n                                                  Ваша заявка прийнята.\r\n                                                </p>\r\n                                              </td>\r\n                                            </tr>\r\n                                          </tbody>\r\n                                        </table>\r\n                                      </td>\r\n                                    </tr>\r\n                                  </tbody>\r\n                                </table>\r\n                              </th>\r\n                            </tr>\r\n                          </tbody>\r\n                        </table>\r\n                      </td>\r\n                    </tr>\r\n                  </tbody>\r\n                </table>\r\n\r\n              </td>\r\n            </tr>\r\n          </tbody>\r\n        </table>\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n";

            await _mailService.SendMailAsync("Your Order", text, userName!);
        }

        public List<OrderDto> GetAll()
        {
            string userId = _httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            List<OrderDto> orders = GetAllById(userId);
            IEnumerable<OrderDto> ordersWithCars = orders.Select(order => GetOrderIncludeCars(order));

            return ordersWithCars.ToList();
        }

        public List<OrderDto> GetAllById(string userId)
        {
            DbSet<Order> orders = _context.Orders;
            IQueryable<Order> userOrders = orders.Where(order => order.UserId.Equals(userId));

            return _mapper.Map<List<OrderDto>>(userOrders.ToList());
        }

        private OrderDto GetOrderIncludeCars(OrderDto order)
        {
            string ids = order.IdsProduct;
            List<CarDto> selectedCars = GetCarsFromIdsString(ids);

            return new OrderDto()
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                Cars = selectedCars
            };
        }

        public List<CarDto> GetCarsFromIdsString(string carIdsString)
        {
            List<CarDto> cars = GetCarList();

            string formattedCarIds = carIdsString
                                        .Replace("[", "")
                                        .Replace("]", "");

            List<string> carIdsStrings = formattedCarIds.Split(',').ToList();
            List<int> carIds = carIdsStrings.Select(id => int.Parse(id)).ToList();
            List<CarDto> selectedCars = carIds.SelectMany(id => cars.Where(car => car.Id == id).Take(1)).ToList();

            return selectedCars;
        }

        public List<CarDto> GetCarList()
        {
            IQueryable<Car> cars = _context.Cars;
            IQueryable<Car> carsWithCats = cars.Include(car => car.Category)!;

            return _mapper.Map<List<CarDto>>(carsWithCats);
        }
    }
}