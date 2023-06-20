using FrontToBack.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FrontToBack.Areas.Adminarea.Controllers
{
    [Area(nameof(Adminarea))]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index(string name)
        {
            var users=name!=null? _userManager.Users.Where(u=>u.FullName.ToLower().Contains(name.ToLower())).ToList(): _userManager.Users.ToList();
            return View(users);
        }
        public async Task<IActionResult>BlockOrActive(string id) 
        {
            var user=await _userManager.FindByIdAsync(id);
            user.IsBlock =! user.IsBlock;
            await _userManager.UpdateAsync(user);
            return RedirectToAction("Index");   
        }
    }
}
