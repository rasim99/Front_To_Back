using Microsoft.AspNetCore.Mvc;

namespace FrontToBack.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
         public IActionResult AddBasket()
        {
            //HttpContext.Session.SetString("p515", "programming group");
            Response.Cookies.Append("p515", "programming group", new CookieOptions { MaxAge = TimeSpan.FromMinutes(15) });
            return Content("set olundu");
        }
        public IActionResult ShowBasket()
        {
            //string result= HttpContext.Session.GetString("p515");
            string result = Request.Cookies["p515"];
            return Content(result);
        }
    }
}
