using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
	public class Category
	{
        [Key]
        public int Id { get; set; }
        [Required, MinLength(5), MaxLength(30)]
        public string Name { get; set; } = default!;
        [Required, MinLength(4), MaxLength(100)]
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
