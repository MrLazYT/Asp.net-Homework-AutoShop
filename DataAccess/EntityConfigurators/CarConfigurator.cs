using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfigurators
{
    internal class CarConfigurator : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(car => car.Id);

            builder
                .HasOne(car => car.Category)
                .WithMany(cat => cat.Cars)
                .HasForeignKey(car => car.CategoryId);

            builder
                .Property(car => car.Model)
                .HasMaxLength(30);
        }
    }
}
