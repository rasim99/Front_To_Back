using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrontToBack.Areas.Adminarea.Controllers
{
    [Area(nameof(Adminarea))]
    //[Authorize (Roles ="Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
