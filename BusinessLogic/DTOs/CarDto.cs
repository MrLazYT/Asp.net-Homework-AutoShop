using DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace BusinessLogic.DTOs
{
    public class CarDto
    {
        public int Id { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile? Image { get; set; }
        public string Model { get; set; } = default!;
        public string Color { get; set; } = default!;
        public int Year { get; set; }
        public int CategoryId { get; set; }
        public int StorageItemId { get; set; }
        public int StorageCount { get; set; }
        public string? CategoryName { get; set; }
        public decimal Price { get; set; }
        public int SoldCount { get; set; }

        public static PropertyInfo GetPropertyInfo(string propertyName)
        {
            Type objectType = typeof(CarDto);
            PropertyInfo property = objectType.GetProperty(propertyName)!;

            return property;
        }
    }
}
