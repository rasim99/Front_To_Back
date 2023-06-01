using FrontToBack.DAL;
using FrontToBack.Models;
using FrontToBack.Sevices;
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
        private readonly ISum  _sum;
        private readonly AccountService _accountService;

        public HomeController(AppDbContext appDbContext,ISum sum, AccountService accountService)
        {
            _appDbContext = appDbContext;
            _sum = sum;
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM();
            homeVM.Sliders = _appDbContext.Sliders.ToList();
            homeVM.SliderContent = _appDbContext.SliderContents.FirstOrDefault();
            homeVM.Categories = _appDbContext.Categories.ToList();
            //homeVM.Products = _appDbContext.Products.Include(p => p.Images).Take(4).ToList();
            homeVM.Blogs = _appDbContext.Blogs.ToList();
            homeVM.Expert = _appDbContext.Experts.FirstOrDefault();
            homeVM.ExpertContents = _appDbContext.ExpertContents.ToList();
            homeVM.Says = _appDbContext.Says.ToList();
            homeVM.Instagrams = _appDbContext.Instagrams.ToList();

            //homeVM.Sliders = _appDbContext.Sliders.AsNoTracking().ToList(); //izlemeden cixarmaq ucun
            //homeVM.SliderContent = _appDbContext.SliderContents.FirstOrDefault();
            //var datas = _appDbContext.ChangeTracker.Entries().ToList(); //butun izlemeleri gore bilerik
            //homeVM.Sliders[0].ImageUrl = "lorem.png"; //ancaq d/bazadan deyismeyecek
            //homeVM.SliderContent.Desc = "lorem ipsim";  //d/bazada deyisecek
            //_appDbContext.SaveChanges();    //butun deyisikleri yadda saxlamaq ucun
            //Blog blog = new();
            //blog.ToString(); //type -ni qaytaracaq

            //ISum sum = new SumService();
            //sum.Sum(3, 7);

            var result = _sum.Sum(10, 20);
            _accountService.Login("Admin", "admin1234");
            return View(homeVM);
        }

    
    }
}