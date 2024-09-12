using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace DataAccess.Models
{
	public class Car
	{
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage = "Model of car can't be empty")]
		public string Model { get; set; } = default!;
		[Required(ErrorMessage = "Color of car can't be empty")]
		public string Color { get; set; } = default!;
        [Required(ErrorMessage = "Year of car can't be empty")]
		[Range(1900, 2100, ErrorMessage = "Year of car must be in range from 1900 to 2100")]
		public int Year { get; set; }
		[Required, ForeignKey("Category")]
		public int CategoryId { get; set; }

		public Category? Category { get; set; }
		
		public static PropertyInfo GetPropertyInfo(string propertyName)
		{
			Type objectType = typeof(Car);
            PropertyInfo property = objectType.GetProperty(propertyName)!;

			return property;
		}
	}
}