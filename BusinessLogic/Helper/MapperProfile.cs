using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.Entities;

namespace BusinessLogic.Helper
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
        }
    }
}
