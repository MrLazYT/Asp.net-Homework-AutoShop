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
					ImagePath = "https://images.dealer.com/autodata/us/color/2020/USD00TOC021D0/040.jpg",
					Model = "Toyota Camry",
					Color = "White",
					Year = 2020,
					CategoryId = 1,
					Price = 28007
                },
				new Car()
				{
					Id = 2,
					ImagePath = "https://di-uploads-pod11.dealerinspire.com/hondaofkirkland/uploads/2019/08/2019-Civic-Type-R-Aegean-Blue.png",
                    Model = "Honda Civic",
					Color = "Blue",
					Year = 2019,
					CategoryId = 1,
					Price = 20000
				},
				new Car()
				{
					Id = 3,
					ImagePath = "https://img.autobytel.com/chrome/colormatched_01/white/640/cc_2021fos10_01_640/cc_2021fos100055_01_640_um.jpg",
					Model = "Ford Explorer",
					Color = "Black",
					Year = 2021,
					CategoryId = 2,
					Price = 32999
				},
				new Car()
				{
					Id = 4,
					ImagePath = "https://dealer-content.s3.amazonaws.com/images/models/chevrolet/2022/tahoe/colors/auburn-metallic.png",
                    Model = "Chevrolet Tahoe",
					Color = "Red",
					Year = 2022,
					CategoryId = 2,
					Price = 52990
				},
				new Car()
				{
					Id = 5,
					ImagePath = "https://static.tcimg.net/vehicles/gallery/1d9ae78296ed295e/2018-BMW-2_Series.jpg",
					Model = "BMW 2 Series",
					Color = "Silver",
					Year = 2018,
					CategoryId = 3,
					Price = 9999
				},
				new Car()
				{
					Id = 6,
					ImagePath = "https://di-uploads-pod5.dealerinspire.com/mercedesbenzofpalmsprings/uploads/2017/05/2017-C-300-Sedan-Selenite-Grey-Metallic-1.png",
					Model = "Mercedes-Benz C-Class",
					Color = "Gray",
					Year = 2017,
					CategoryId = 3,
					Price = 9300
				},
				new Car()
				{
					Id = 7,
					ImagePath = "https://www.miataforumz.com/attachments/miata-general-discussion-36/1958d1317909812-mazda-mx-5-miata-gets-black-tuned-japan-black-tuned-roadster-628.jpg",
					Model = "Mazda MX-5",
					Color = "Green",
					Year = 2021,
					CategoryId = 4,
					Price = 27999
				},
				new Car()
				{
					Id = 8,
					ImagePath = "https://i.ebayimg.com/images/g/BsAAAOSwpaZbmWIz/s-l1200.jpg",
					Model = "Ford Mustang",
					Color = "Yellow",
					Year = 2020,
					CategoryId = 4,
					Price = 65000
				},
				new Car()
				{
					Id = 9,
					ImagePath = "https://www.stablevehiclecontracts.co.uk/uploads/2-2020-vw-golf-gti-mk8-pure-white.jpg",
					Model = "Volkswagen Golf",
					Color = "White",
					Year = 2019,
					CategoryId = 5,
					Price = 23000
				},
				new Car()
				{
					Id = 10,
					ImagePath = "https://platform.cstatic-images.com/xlarge/in/v2/stock_photos/4f7a628e-4f6c-4ea4-b32f-3bdb82cbd18b/dd77a9fa-afc6-4dc4-baa2-bdddb8383dac.png",
					Model = "Honda Fit",
					Color = "Black",
					Year = 2020,
					CategoryId = 5,
					Price = 17100
				},
			});
		}
	}
}
