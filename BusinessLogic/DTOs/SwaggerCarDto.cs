namespace BusinessLogic.DTOs
{
    public class SwaggerCarDto
    {
        public string? ImagePath { get; set; }
        public string Model { get; set; } = default!;
        public string Color { get; set; } = default!;
        public int Year { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public decimal Price { get; set; }
        public int SoldCount { get; set; }
    }
}
