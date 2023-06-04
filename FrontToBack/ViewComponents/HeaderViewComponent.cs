using FrontToBack.DAL;
using FrontToBack.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FrontToBack.ViewComponents
{
    public class HeaderViewComponent :ViewComponent
    {
        private readonly AppDbContext _appDbContext;

        public HeaderViewComponent(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string basket = Request.Cookies["basket"];
            ViewBag.BasketCount = 0;
            if (basket != null)
            {
                var products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
                ViewBag.BasketCount = products.Count;
            }
            var bio = _appDbContext.Bios.FirstOrDefault();
            return View(await Task.FromResult(bio));
        }
    }
}
