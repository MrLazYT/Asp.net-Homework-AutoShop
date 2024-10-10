using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfigurators
{
    public class StorageItemConfigurator : IEntityTypeConfiguration<StorageItem>
    {
        public void Configure(EntityTypeBuilder<StorageItem> builder)
        {
            builder
                .HasKey(storageItem => storageItem.Id);

            builder
                .HasOne(storageItem => storageItem.Car)
                .WithOne(car => car.StorageItem)
                .HasForeignKey<StorageItem>(storageItem => storageItem.CarId)
                .HasPrincipalKey<Car>(car => car.StorageItemId);
        }
    }
}
