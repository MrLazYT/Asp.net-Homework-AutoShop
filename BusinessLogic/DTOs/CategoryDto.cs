
namespace BusinessLogic.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;

        public override string ToString()
        {
            return Name;
        }
    }
}
