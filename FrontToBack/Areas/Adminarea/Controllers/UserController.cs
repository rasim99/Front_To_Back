using FrontToBack.DAL;
using FrontToBack.Models;
using FrontToBack.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FrontToBack.Areas.Adminarea.Controllers
{
    [Area(nameof(Adminarea))]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
		public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public IActionResult Index(string name)
        {
            var users=name!=null? _userManager.Users.Where(u=>u.FullName.ToLower().Contains(name.ToLower())).ToList(): _userManager.Users.ToList();
            return View(users);
        }
		public async Task<IActionResult> Detail(string id)
		{
            var user =await  _userManager.FindByIdAsync(id);
			var userRoles = await _userManager.GetRolesAsync(user);
			var roles = _roleManager.Roles.ToList();
			return View(new RoleUpdateVM
			{
				Roles = roles,
				User = user,
				UserRoles = userRoles
			});
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
