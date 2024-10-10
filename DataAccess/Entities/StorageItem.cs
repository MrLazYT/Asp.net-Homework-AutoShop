namespace DataAccess.Entities
{
    public class StorageItem
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int Count { get; set; }

        public Car Car { get; set; }
    }
}
