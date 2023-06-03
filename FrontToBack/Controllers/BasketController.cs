using FrontToBack.DAL;
using FrontToBack.Models;
using FrontToBack.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FrontToBack.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public BasketController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
         public IActionResult AddBasket(int ?id)
        {
            if (id == null) return NotFound();
            var product = _appDbContext.Products.Find(id);
            if (product == null) return NotFound();
            var basket = Request.Cookies["basket"];
            List<BasketVM> products;
            if(basket == null)
            {
                 products= new List<BasketVM>();
            }
            else
            {
                products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            }
            var existProduct = products.Find(x => x.Id == product.Id);
            if (existProduct == null)
            {
                BasketVM basketVM = new BasketVM()
                {
                    Id = product.Id,
                    Name = product.Name,
                    BasketCount=1
                };
                products.Add(basketVM);

            }
            else
            {
                existProduct.BasketCount++;
            }
         
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromMinutes(10) });
            return Content($" {product.Name} set olundu");
        }
        public IActionResult ShowBasket()
        {
            var basket = Request.Cookies["basket"];
            var result=JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            return Json(result);
        }
    }
}
