using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Reflection;

namespace BusinessLogic.Services
{
    public class CarService : IEntityService<CarDto>
    {
        private readonly CarContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public CarService(CarContext context, IMapper mapper, IFileService fileService)
        {
            _context = context;
            _mapper = mapper;
            _fileService = fileService;
        }

        public CarDto GetById(int id)
        {
            List<CarDto> cars = GetAll();

            CarDto car = cars.FirstOrDefault(c => c.Id == id)!;

            return car;
        }

        public List<CarDto> GetAll()
        {
            DbSet<Car> cars = _context.Cars;
            IIncludableQueryable<Car, StorageItem> carsWithIncludes = cars
                .Include(car => car.Category)!
                .Include(car => car.StorageItem)!;

            return _mapper.Map<List<CarDto>>(carsWithIncludes);
        }

        public List<CarDto> GetAllByCategory(int categoryId)
        {
            List<CarDto> cars = GetAll();

            if (categoryId != 0)
            {
                IEnumerable<CarDto> filteredCars = cars.Where(car => car.CategoryId == categoryId);

                return filteredCars.ToList();
            }

            return cars;
        }

        public void Add(CarDto carDto)
        {
            if (carDto != null)
            {
                if (carDto.Image != null)
                {
                    carDto.ImagePath = _fileService.SaveProductImage(carDto.Image).Result;
                }

                Car car = _mapper.Map<Car>(carDto);

                _context.Cars.Add(car);
                _context.SaveChanges();
            }
        }

        public void Update(CarDto carDto)
        {
            CarDto carOld = GetById(carDto.Id);

            if (carOld != null && carDto != null)
            {
                if (carDto.Image != null)
                {
                    carDto.ImagePath = _fileService.EditProductImage(carDto.ImagePath, carDto.Image).Result;
                }

                Car car = _mapper.Map<Car>(carDto);

                _context.Cars.Update(car);
                _context.SaveChanges();
            }
        }

        public void Delete(CarDto carDto)
        {
            if (carDto != null)
            {
                if (carDto.ImagePath != null)
                {
                    _fileService.DeleteProductImage(carDto.ImagePath);
                }

                Car car = _mapper.Map<Car>(carDto);

                _context.Cars.Remove(car);
                _context.SaveChanges();
            }
        }

        public List<CarDto> GetTopFive()
        {
            List<CarDto> cars = GetAll();
            IOrderedEnumerable<CarDto> sortedBySoldCountCars = cars.OrderByDescending(car => car.SoldCount);
            IEnumerable<CarDto> topFive = sortedBySoldCountCars.Take(5);

            return _mapper.Map<List<CarDto>>(topFive);
        }

        public List<CarDto> GetAllSorted(List<CarDto> cars, PropertyInfo property)
        {
            return SortAllUsingFunc(Sort, cars, property);
        }

        public List<CarDto> GetAllSortedDesc(List<CarDto> cars, PropertyInfo property)
        {
            return SortAllUsingFunc(SortDesc, cars, property);
        }

        private List<CarDto> SortAllUsingFunc(
            Func<List<CarDto>, PropertyInfo, IOrderedEnumerable<CarDto>> func,
            List<CarDto> cars,
            PropertyInfo property)
        {
            if (property != null)
            {
                IOrderedEnumerable<CarDto> orderedCars = func.Invoke(cars, property);

                return orderedCars.ToList();
            }

            return cars;
        }

        private IOrderedEnumerable<CarDto> Sort(List<CarDto> cars, PropertyInfo property)
        {
            return cars.OrderBy(car => property.GetValue(car, null));
        }

        private IOrderedEnumerable<CarDto> SortDesc(List<CarDto> cars, PropertyInfo property)
        {
            return cars.OrderByDescending(car => property.GetValue(car, null));
        }
    }
}
