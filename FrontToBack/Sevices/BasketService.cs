using FrontToBack.ViewModels;
using Newtonsoft.Json;

namespace FrontToBack.Sevices
{
    public class BasketService : IBasketService
    {
        IHttpContextAccessor _contextAccessor;

        public BasketService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public int BasketCount()
        {
            string basket = _contextAccessor.HttpContext.Request.Cookies["basket"];
            if (basket != null)
            {
                var products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
                return products.Count();
            }
            return 0;
        }
    }
}
