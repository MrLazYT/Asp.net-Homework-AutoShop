using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.Entities;

namespace BusinessLogic.Helpers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Car, CarDto>()
                .ForMember(carDto => carDto.CategoryName, opt => opt.MapFrom(car => car.Category!.Name))
                .ForMember(carDto => carDto.StorageCount, opt => opt.MapFrom(car => car.StorageItem!.Count));

            CreateMap<CarDto, Car>();

            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<Order, OrderDto>()
                .ForMember(orderDto => orderDto.Cars, opt => opt.Ignore());

            CreateMap<OrderDto, Order>();
        }
    }
}
