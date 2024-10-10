using BusinessLogic.Extensions;
using Microsoft.AspNetCore.Http;

namespace BusinessLogic.Services
{
    public class SessionData
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private HttpContext HttpContext { get; set; }

        public SessionData(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            HttpContext = _httpContextAccessor.HttpContext!;
        }

        public Dictionary<int, int> GetCartData()
        {
            return HttpContext.Session.GetObject<Dictionary<int, int>>("cart") ?? new Dictionary<int, int>();
        }

        public void SetCartData(Dictionary<int, int> cartData)
        {
            HttpContext.Session.SetObject<Dictionary<int, int>>("cart", cartData);
        }

        public void ClearData()
        {
            HttpContext.Session.Clear();
        }
    }
}
