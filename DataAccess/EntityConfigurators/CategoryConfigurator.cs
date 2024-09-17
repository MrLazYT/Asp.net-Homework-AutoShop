using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfigurators
{
    internal class CategoryConfigurator : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(cat => cat.Id);

            /*builder
                .HasMany(cat => cat.Cars)
                .WithOne(car => car.Category)
                .HasForeignKey(car => car.CategoryId);*/

            builder
                .Property(cat => cat.Name)
                .HasMaxLength(20);

            builder
                .Property(cat => cat.Description)
                .HasMaxLength(150);
        }
    }
}