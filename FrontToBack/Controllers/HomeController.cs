using FrontToBack.DAL;
using FrontToBack.Models;
using FrontToBack.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;

namespace FrontToBack.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext  _appDbContext   ;

        public HomeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM();
            homeVM.Sliders = _appDbContext.Sliders.ToList();
            homeVM.SliderContent = _appDbContext.SliderContents.FirstOrDefault();
            homeVM.Categories = _appDbContext.Categories.ToList();
            homeVM.Products = _appDbContext.Products.Include(p => p.Images).ToList();
            homeVM.Blogs = _appDbContext.Blogs.ToList();
            homeVM.Expert = _appDbContext.Experts.FirstOrDefault();
            homeVM.ExpertContents = _appDbContext.ExpertContents.ToList();
            homeVM.Says = _appDbContext.Says.ToList();
            homeVM.Instagrams = _appDbContext.Instagrams.ToList();
            return View(homeVM);
        }

    
    }
}