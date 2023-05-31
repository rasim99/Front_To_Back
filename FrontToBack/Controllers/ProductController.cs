using FrontToBack.DAL;
using FrontToBack.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrontToBack.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ProductController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {

            var query = _appDbContext.Products.AsQueryable();
            var products=query.Include(p => p.Category)
            .Include(p => p.Images)
            .Take(2)
            .ToList();
            //var products = _appDbContext.Products
            //    .Include(p=>p.Category)
            //    .Include(p => p.Images)
            //    .Take(2)
            //    .ToList();
            ViewBag.ProductCount = query.Count();
            return View(products);
        }
        public IActionResult loadMore( int skip)
        {
            //map etme 1
            //var products= _appDbContext.Products.Include(p=>p.Images).Include(p=>p.Category).ToList();
            // List<LoadMoreVM> list = new();

            // foreach (var item in products)
            // {
            //     LoadMoreVM loadMoreVM = new();
            //     loadMoreVM.Id = item.Id;
            //     loadMoreVM.Name = item.Name;
            //     loadMoreVM.Price = item.Price;
            //     loadMoreVM.CategoryName = item.Category.Name;
            //     loadMoreVM.ImageUrl = item.Images.FirstOrDefault(i => i.IsMain).ImageUrl;
            //     list.Add(loadMoreVM);
            // }

            //map etme 2
            //var loadmore = _appDbContext.Products
            //    .Select(p => new LoadMoreVM
            //    {
            //        Id = p.Id,
            //        Name=p.Name,
            //        Price = p.Price,
            //        CategoryId=p.CategoryId,
            //        CategoryName=p.Category.Name,
            //        ImageUrl=p.Images.FirstOrDefault(i=>i.IsMain).ImageUrl
            //    }).ToList();
            //return Json(loadmore);
            var products = _appDbContext.Products
                .Include(p => p.Category)
                .Include(p => p.Images)
                .Skip(skip)
                .Take(2)
                .ToList();
            return PartialView("_LoadMorePartial", products);
        }

        public IActionResult Search(string search)
        {
            var products = _appDbContext.Products
                .Where(p => p.Name.ToLower().Contains(search.ToLower()))
                .Take(2)
                .OrderByDescending(p=>p.Id)
                .ToList();

            return PartialView("_SearchPartial", products);
        }
    }
}
