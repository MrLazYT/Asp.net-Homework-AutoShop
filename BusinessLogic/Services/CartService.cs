using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using DataAccess.Data;
using AutoMapper;

namespace BusinessLogic.Services
{
    public class CartService : IEntityService<CarDto>
    {
        private readonly CarContext _context;
        private readonly SessionData _sessionData;
        private readonly IMapper _mapper;

        public CartService(CarContext context, SessionData sessionData, IMapper mapper)
        {
            _context = context;
            _sessionData = sessionData;
            _mapper = mapper;
        }

        public CarDto GetById(int id)
        {
            List<CarDto> cars = GetAll();

            return cars.FirstOrDefault(car => car.Id == id)!;
        }

        public List<CarDto> GetAllFromCart()
        {
            Dictionary<int, int> idsAndQuantities = _sessionData.GetCartData();
            List<int> ids = idsAndQuantities.Keys.ToList();
            List<CarDto> cars = GetAll();
            List<CarDto> carsInCart = null!;

            if (cars != null)
            {
                carsInCart = cars.Where(car => ids.Contains(car.Id)).ToList();
            }

            return _mapper.Map<List<CarDto>>(carsInCart);
        }

        public List<CarDto> GetAll()
        {
            DbSet<Car> cars = _context.Cars;
            IIncludableQueryable<Car, StorageItem> carsWithCat = cars
                .Include(car => car.Category)!
                .Include(car => car.StorageItem)!;

            return _mapper.Map<List<CarDto>>(carsWithCat);
        }

        public void Add(CarDto car)
        {
            InvokeFunc(null!, car.Id);
        }

        public void Update(CarDto car)
        {
            throw new NotImplementedException();
        }

        public void Delete(CarDto car)
        {
            InvokeFunc(RemoveFromCart, car.Id);
        }

        public void PlusQuantity(int id)
        {
            InvokeFunc(PlusQuantityDelegate, id);
        }

        public void MinusQuantity(int id)
        {
            InvokeFunc(MinusQuantityDelegate, id);
        }

        private void InvokeFunc(Func<int, Dictionary<int, int>, Dictionary<int, int>> func, int id)
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
        }

        private Dictionary<int, int> RemoveFromCart(int id, Dictionary<int, int> idsAndQuantities)
        {
            idsAndQuantities.Remove(id);

            return idsAndQuantities;
        }

        private Dictionary<int, int> PlusQuantityDelegate(int id, Dictionary<int, int> idsAndQuantities)
        {
            CarDto car = GetById(id);
            int storageCount = car.StorageCount;
            int quantity = idsAndQuantities[id];

            if (quantity < storageCount)
            {
                idsAndQuantities[id] = ++quantity;
            }

            return idsAndQuantities;
        }

        private Dictionary<int, int> MinusQuantityDelegate(int id, Dictionary<int, int> idsAndQuantities)
        {
            int quantity = idsAndQuantities[id];

            if (quantity != 1)
            {
                idsAndQuantities[id] = --quantity;
            }

            return idsAndQuantities;
        }
    }
}
