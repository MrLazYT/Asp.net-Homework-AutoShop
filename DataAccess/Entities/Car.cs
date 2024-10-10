namespace DataAccess.Entities
{
	public class Car
	{
		public int Id { get; set; }
		public string ImagePath { get; set; } = default!;
		public string Model { get; set; } = default!;
		public string Color { get; set; } = default!;
		public int Year { get; set; }
		public int CategoryId { get; set; }
		public int StorageItemId { get; set; }
		public decimal Price { get; set; }
		public int SoldCount { get; set; }

		public Category? Category { get; set; }
		public StorageItem? StorageItem { get; set; }

        public override string ToString()
        {
			return Model;
        }
    }
}