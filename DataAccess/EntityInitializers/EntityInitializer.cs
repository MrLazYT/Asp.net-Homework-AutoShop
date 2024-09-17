using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityInitializers
{
	public interface EntityInitializer<T> where T : class
	{
		public static void SeedData(EntityTypeBuilder<T> builder) { }
	}
}