using Microsoft.AspNetCore.Mvc;

namespace FrontToBack.Areas.Adminarea.Controllers
{
    [Area(nameof(Adminarea))]

    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
