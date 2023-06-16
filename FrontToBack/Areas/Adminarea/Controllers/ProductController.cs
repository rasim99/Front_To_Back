using FrontToBack.DAL;
using FrontToBack.Helper;
using FrontToBack.Models;
using FrontToBack.ViewModels.AdminVM.Category;
using FrontToBack.ViewModels.AdminVM.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FrontToBack.Areas.Adminarea.Controllers
{
    [Area(nameof(Adminarea))]
    public class ProductController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
          
        }

        public IActionResult Index()
        {
            var products = _appDbContext.Products
                .Include(i => i.Category)
                .Include(i => i.Images)
                .ToList();
            return View(products);
        }
        public IActionResult Create()
        {
            //ViewBag.Category = _appDbContext.Categories.ToList();
            ViewBag.Category = new SelectList(_appDbContext.Categories.ToList(),"Id", "Name");
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(ProductCreateVM productCreateVM)
        {
            if (!ModelState.IsValid) return NotFound();
            Product product = new();
            foreach (var item in productCreateVM.Photos)
            {
                if (!item.CheckFileType())
                {
                    ModelState.AddModelError("Photos", "wrong format");
                    return View();
                }
                if (item.ChechkFileSize(1000))
                {
                    ModelState.AddModelError("Photos", "small size");
                    return View();
                }
                Image image = new();
                if (item ==productCreateVM.Photos[0])
                {
                    image.IsMain = true;
                }
                image.ImageUrl = item.SaveImage(_webHostEnvironment, "img");
                product.Images.Add(image);
            }
            product.Name = productCreateVM.Name;
            product.Price = productCreateVM.Price;
            product.Count = productCreateVM.Count;
            product.CategoryId = productCreateVM.CategoryId;
            _appDbContext.Products.Add(product);
            _appDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Detail(int? id)
        {
            if (id == null) return NotFound();
            var product = _appDbContext.Products.Include(v=>v.Category)
                .Include(v=>v.Images).FirstOrDefault(i => i.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }
        public IActionResult Delete(int?id)
        {
            if (id == null) return NotFound();
            var product = _appDbContext.Products.Include(v => v.Category)
               .Include(v => v.Images).FirstOrDefault(i => i.Id == id);
            if (product == null) return NotFound();
            foreach (var item in product.Images)
            {
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", item.ImageUrl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            _appDbContext.Products.Remove(product);
            _appDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {

            if (id == null) return NotFound();
            var product = _appDbContext.Products
            .Include(p=>p.Category)
            .Include(p=>p.Images)
               . FirstOrDefault(i => i.Id == id);
            if (product == null) return NotFound();
            ViewBag.Category = new SelectList(_appDbContext.Categories.ToList(), "Id", "Name");
            //string filename = _appDbContext.Images.FirstOrDefault(i=>i.Id == id).ImageUrl;
            //ViewBag.FileName = filename;      (error)
            return View();
           
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(int? id, ProductUpdateVM productUpdateVM)
        {
            if (!ModelState.IsValid) return NotFound();
            var product = _appDbContext.Products.FirstOrDefault(c => c.Id == id);
            var exist = _appDbContext.Products.Any(c => c.Name.ToLower() == product.Name.ToLower() && c.Id != id);
            if (exist)
            {
                ModelState.AddModelError("Name", "eyni adli category vardir");
                return View();
            }

            foreach (var item in productUpdateVM.Photos)
            {
                if (!item.CheckFileType())
                {
                    ModelState.AddModelError("Photos", "wrong format");
                    return View();
                }
                if (item.ChechkFileSize(1000))
                {
                    ModelState.AddModelError("Photos", "small size");
                    return View();
                }
                Image image = new();
                if (item == productUpdateVM.Photos[0])
                {
                    image.IsMain = true;
                }
                image.ImageUrl = item.SaveImage(_webHostEnvironment, "img");
                product.Images.Add(image);
            }
            product.Name = productUpdateVM.Name;
            product.Price = productUpdateVM.Price;
            product.Count = productUpdateVM.Count;

            
            _appDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
