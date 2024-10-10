using DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityInitializers
{
    public class StorageItemInitializer : EntityInitializer<StorageItem>
    {
        public static void SeedData(EntityTypeBuilder<StorageItem> builder)
        {
            builder.HasData(new List<StorageItem>()
            {
                new StorageItem()
                {
                    Id = 1,
                    CarId = 1,
                    Count = 0
                },
                new StorageItem()
                {
                    Id = 2,
                    CarId = 2,
                    Count = 2
                },
                new StorageItem()
                {
                    Id = 3,
                    CarId = 3,
                    Count = 2
                },
                new StorageItem()
                {
                    Id = 4,
                    CarId = 4,
                    Count = 2
                },
                new StorageItem()
                {
                    Id = 5,
                    CarId = 5,
                    Count = 2
                },
                new StorageItem()
                {
                    Id = 6,
                    CarId = 6,
                    Count = 2
                },
                new StorageItem()
                {
                    Id = 7,
                    CarId = 7,
                    Count = 2
                },
                new StorageItem()
                {
                    Id = 8,
                    CarId = 8,
                    Count = 2
                },
                new StorageItem()
                {
                    Id = 9,
                    CarId = 9,
                    Count = 5
                },
                new StorageItem()
                {
                    Id = 10,
                    CarId = 10,
                    Count = 2
                },
            });
        }
    }
}
