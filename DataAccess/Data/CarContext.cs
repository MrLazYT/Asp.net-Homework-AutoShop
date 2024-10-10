using DataAccess.EntityInitializers;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataAccess.Data
{
	public class CarContext : IdentityDbContext
	{
        public DbSet<Car> Cars { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<StorageItem> StorageItems { get; set; }

        public CarContext(DbContextOptions<CarContext> options) : base(options)
		{
            // ОК :)
			//Database.EnsureCreated();
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            EntityTypeBuilder<Category> categoryBuilder = modelBuilder.Entity<Category>();
            EntityTypeBuilder<Car> carBuilder = modelBuilder.Entity<Car>();
            EntityTypeBuilder<StorageItem> storageItemBuilder = modelBuilder.Entity<StorageItem>();

            CategoryInitializer.SeedData(categoryBuilder);
            CarInitializer.SeedData(carBuilder);
            StorageItemInitializer.SeedData(storageItemBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarContext).Assembly);
        }
    }
}