using FrontToBack.DAL;
using FrontToBack.Models;
using FrontToBack.ViewModels.AdminVM.Category;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace FrontToBack.Areas.Adminarea.Controllers
{
    [Area(nameof(Adminarea))]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public CategoryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            return View(_appDbContext.Categories.ToList());
        }
        
        public IActionResult Detail(int ?id)
        {
            if (id == null) return NotFound();
            var category = _appDbContext.Categories.FirstOrDefault(i => i.Id == id);
            if (category == null) return NotFound();
            return View(category);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create( CategoryVM categoryVM )
        {
            if (!ModelState.IsValid) return View();
            var exist= _appDbContext.Categories.Any(c => c.Name.ToLower() == categoryVM.Name.ToLower());
            if (exist)
            {
                ModelState.AddModelError("Name", "eyni adli category vardir");
                return View();
            }
            Category newcategory = new();
            newcategory.Name = categoryVM.Name;
            newcategory.Desc = categoryVM.Desc;
            _appDbContext.Categories.Add(newcategory);
            _appDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int?id)
        {

            if (id == null) return NotFound();
            var category = _appDbContext.Categories.FirstOrDefault(i => i.Id == id);
            if (category == null) return NotFound();
            return View(new CategoryVM { Name=category.Name , Desc=category.Name});
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(int ?id, CategoryVM categoryVM)
        {
            if (!ModelState.IsValid) return NotFound();
            var category = _appDbContext.Categories.FirstOrDefault(c => c.Id == id);
            var exist = _appDbContext.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower() && c.Id!=id);
            if (exist)
            {
                ModelState.AddModelError("Name", "eyni adli category vardir");
                return View();
            }
            category.Name = categoryVM.Name;
            category.Desc = categoryVM.Desc;
            _appDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
   
         public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var category = _appDbContext.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            _appDbContext.Categories.Remove(category);
            _appDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
