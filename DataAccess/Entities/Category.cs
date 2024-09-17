using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
	public class Category
	{
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;

        public ICollection<Car> Cars { get; set; }

		public Category()
        {
            Cars = new HashSet<Car>();
        }

		public override string ToString()
		{
			return Name;
		}
	}
}
