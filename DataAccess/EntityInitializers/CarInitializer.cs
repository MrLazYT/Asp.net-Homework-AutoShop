using DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityInitializers
{
    public class CarInitializer : EntityInitializer<Car>
	{
        public static void SeedData(EntityTypeBuilder<Car> builder)
		{
			builder.HasData(new List<Car>()
			{
				new Car()
				{
					Id = 1,
					Model = "Toyota Camry",
					Color = "White",
					Year = 2020,
					CategoryId = 1
				},
				new Car()
				{
					Id = 2,
					Model = "Honda Civic",
					Color = "Blue",
					Year = 2019,
					CategoryId = 1
				},
				new Car()
				{
					Id = 3,
					Model = "Ford Explorer",
					Color = "Black",
					Year = 2021,
					CategoryId = 2
				},
				new Car()
				{
					Id = 4,
					Model = "Chevrolet Tahoe",
					Color = "Red",
					Year = 2022,
					CategoryId = 2
				},
				new Car()
				{
					Id = 5,
					Model = "BMW 2 Series",
					Color = "Silver",
					Year = 2018,
					CategoryId = 3
				},
				new Car()
				{
					Id = 6,
					Model = "Mercedes-Benz C-Class",
					Color = "Gray",
					Year = 2017,
					CategoryId = 3
				},
				new Car()
				{
					Id = 7,
					Model = "Mazda MX-5",
					Color = "Green",
					Year = 2021,
					CategoryId = 4
				},
				new Car()
				{
					Id = 8,
					Model = "Ford Mustang",
					Color = "Yellow",
					Year = 2020,
					CategoryId = 4
				},
				new Car()
				{
					Id = 9,
					Model = "Volkswagen Golf",
					Color = "White",
					Year = 2019,
					CategoryId = 5
				},
				new Car()
				{
					Id = 10,
					Model = "Honda Fit",
					Color = "Black",
					Year = 2020,
					CategoryId = 5
				},
			});
		}
	}
}
