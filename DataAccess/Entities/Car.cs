using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace DataAccess.Entities
{
	public class Car
	{
		public int Id { get; set; }
		public string Model { get; set; } = default!;
		public string Color { get; set; } = default!;
		public int Year { get; set; }
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