using FrontToBack.DAL;
using FrontToBack.Models;
using FrontToBack.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FrontToBack.ViewComponents
{
    public class HeaderViewComponent :ViewComponent
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;

      

        public HeaderViewComponent(AppDbContext appDbContext, UserManager<AppUser> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.UserFullName = "";
            if (User.Identity.IsAuthenticated)
            {
                AppUser user=await _userManager.FindByNameAsync(User.Identity.Name);
                ViewBag.UserFullName = user.FullName;
            }
            var bio = _appDbContext.Bios.FirstOrDefault();
            return View(await Task.FromResult(bio));
        }
    }
}
