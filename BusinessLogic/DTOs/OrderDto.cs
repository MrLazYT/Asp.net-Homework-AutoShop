using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string IdsProduct { get; set; } = default!;
        public decimal TotalPrice { get; set; }
        public string UserId { get; set; } = default!;
        public List<CarDto> Cars { get; set; } = default!;
    }
}
