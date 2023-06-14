using FrontToBack.DAL;
using FrontToBack.Models;
using FrontToBack.ViewModels.AdminVM.Slider;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace FrontToBack.Areas.Adminarea.Controllers
{
    [Area(nameof(Adminarea))]
    public class SliderController : Controller
    {
      
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

   

        public SliderController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View(_appDbContext.Sliders.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(SliderCreateVM sliderCreateVM)
        {
            if (sliderCreateVM.Photo == null)
            {
                ModelState.AddModelError("Photo", "dont be empty");
                return View();
            }
            if (!sliderCreateVM.Photo.ContentType.Contains("image"))
            {
                ModelState.AddModelError("Photo", "Wrong Format");
                return View();
            }
            if (sliderCreateVM.Photo.Length < 1000)
            {
                ModelState.AddModelError("Photo", " Size is small");
                return View();
            }

            string fileName = Guid.NewGuid() + sliderCreateVM.Photo.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);
            using (FileStream fileStream=new FileStream(path, FileMode.Create))
            {
                sliderCreateVM.Photo.CopyTo(fileStream);
            }
            Slider slider = new();
            slider.ImageUrl = fileName;
            _appDbContext.Sliders.Add(slider);
            _appDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
