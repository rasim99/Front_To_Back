using FrontToBack.Hubs;
using FrontToBack.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FrontToBack.Controllers
{
    public class ChatController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHubContext<ChatHub> _hubContext;
		public ChatController(UserManager<AppUser> userManager, IHubContext<ChatHub> hubContext)
		{
			_userManager = userManager;
			_hubContext = hubContext;
		}

		public IActionResult Chat()
        {
            //_hubContext.Clients.Client("");
            //_hubContext.Clients.Clients();
            ViewBag.Users = _userManager.Users.ToList();
            return View();
        }
        public async Task<IActionResult> ShowAlert(string userId)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
           await  _hubContext.Clients.Client(user.ConnectionId).SendAsync("ShowAlert", user.FullName);
            return Content("sended");
        }
    }
}
