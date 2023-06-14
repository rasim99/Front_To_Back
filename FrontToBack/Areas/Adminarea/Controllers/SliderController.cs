using FrontToBack.DAL;
using FrontToBack.Helper;
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
            if (!sliderCreateVM.Photo.CheckFileType())
            {
                ModelState.AddModelError("Photo", "Wrong Format");
                return View();
            }
            if (sliderCreateVM.Photo.ChechkFileSize(1000))
            {
                ModelState.AddModelError("Photo", " Size is small");
                return View();
            }
            Slider slider = new();
            slider.ImageUrl = sliderCreateVM.Photo.SaveImage(_webHostEnvironment,"img");
            _appDbContext.Sliders.Add(slider);
            _appDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var slider = _appDbContext.Sliders.FirstOrDefault(s => s.Id == id);
            if(slider==null) return NotFound();
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", slider.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            _appDbContext.Sliders.Remove(slider);
            _appDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Detail(int? id)
        {
            if (id == null) return NotFound();
            var slider = _appDbContext.Sliders.FirstOrDefault(i => i.Id == id);
            if (slider == null) return NotFound();
            return View(slider);
        }
    }
}
