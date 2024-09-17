using DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityInitializers
{
	public class CategoryInitializer : EntityInitializer<Category>
	{
		public static void SeedData(EntityTypeBuilder<Category> builder)
		{
			
			builder.HasData(new List<Category>()
			{
				new Category()
				{
					Id = 1,
					Name = "Sedan",
					Description = "Standard 4-door passenger cars"
				},
				new Category()
				{
					Id = 2,
					Name = "SUV",
					Description = "Sport Utility Vehicles"
				},
				new Category()
				{
					Id = 3,
					Name = "Coupe",
					Description = "Two-door sports cars"
				},
				new Category()
				{
					Id = 4,
					Name = "Convertible",
					Description = "Cars with retractable roofs"
				},
				new Category()
				{
					Id = 5,
					Name = "Hatchback",
					Description = "Compact cars with hatch-style trunk"
				},
			});
		}
	}
}
