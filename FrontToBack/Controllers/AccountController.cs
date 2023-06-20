using FrontToBack.Helper;
using FrontToBack.Models;
using FrontToBack.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FrontToBack.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = new ();
            user.FullName=registerVM.FullName; 
            user.Email=registerVM.Email;
            user.UserName=registerVM.UserName;
         IdentityResult result=   await   _userManager.CreateAsync(user,registerVM.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(registerVM);
            }
            await _userManager.AddToRoleAsync(user, RoleEnums.Member.ToString());
            return RedirectToAction("index", "home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login");
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM,string?ReturnUrl)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = await _userManager.FindByNameAsync(loginVM.UserNameOrEmail);
            if (user==null)
            {
                user = await _userManager.FindByEmailAsync(loginVM.UserNameOrEmail);
                
            }
            if (user==null)
            {
                ModelState.AddModelError("", "username or password wrong..");
                return View(loginVM);

            }

            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password,loginVM.RememberMe,true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "hesab bloklanmisdir");
                return View(loginVM);
            }
            if (user.IsBlock)
            {
                ModelState.AddModelError("", "girisinize qadagan olunmusdur ");
                return View(loginVM);
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "username or password wrong..");
                return View(loginVM);
            }

            await _signInManager.SignInAsync(user, loginVM.RememberMe);

            if (ReturnUrl!=null)
            {
                return Redirect(ReturnUrl);
            }
             var userList=await _userManager.GetRolesAsync(user);
            
            if (userList.Contains(RoleEnums.Admin.ToString()))
            {
                return RedirectToAction("index","dashboard",new {area="Adminarea"});
            }

            return RedirectToAction("index", "home");
        }
        public  async Task<IActionResult> AddRole()
        {
            foreach (var item in Enum.GetValues(typeof(RoleEnums)))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name =item.ToString() });
            }

            return Content("added");
        }
    }
}
