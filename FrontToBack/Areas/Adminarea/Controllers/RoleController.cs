using FrontToBack.DAL;
using FrontToBack.Models;
using FrontToBack.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FrontToBack.Areas.Adminarea.Controllers
{
    [Area(nameof(Adminarea))]
    public class RoleController : Controller
    {
        private readonly AppDbContext _appdbcontext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        public RoleController(AppDbContext appdbcontext, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _appdbcontext = appdbcontext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_roleManager.Roles.ToList()); 
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string roleName)
        {
            if (string.IsNullOrEmpty(roleName)) return BadRequest();
            await _roleManager.CreateAsync(new IdentityRole { Name= roleName });
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(string id)
        {
            _roleManager.DeleteAsync(await _roleManager.FindByIdAsync(id));
            return RedirectToAction("Index");
        }
        public async  Task<IActionResult> Update(string id)
        {
          var user=await  _userManager.FindByIdAsync(id);
            
            var userRoles=await _userManager.GetRolesAsync(user);
            var roles=_roleManager.Roles.ToList();
            return View(new RoleUpdateVM
            {
                Roles=roles,
                User=user,
                UserRoles=userRoles
            });
        }
        [HttpPost]
        public async Task<IActionResult> Update(string id, List<string> newRoles)
        {
            var user = await _userManager.FindByIdAsync(id);
            var userRoles =await  _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles);
            await _userManager.AddToRolesAsync(user, newRoles);
            return Content("updated");
        }
       
    
    }
}
