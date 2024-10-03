using Microsoft.AspNetCore.Identity;

namespace DataAccess.Entities
{
    public class User : IdentityUser
    {
        public DateTime BirthDate { get; set; }

        public ICollection<Order> Orders { get; set; }

        public User() : base()
        {
            Orders = new HashSet<Order>();
        }
    }
}