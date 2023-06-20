using FrontToBack.Models;
using Microsoft.AspNetCore.Identity;

namespace FrontToBack.ViewModels
{
	public class UserDetailVM
	{
		public AppUser User { get; set; }
		public List<IdentityRole> Roles { get; set; }
		public IList<string> UserRoles { get; set; }
	}
}
