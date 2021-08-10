using Hotel.WEB.Additional;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hotel.WEB.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(Roles.User));
            result = await _roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            return View(_roleManager.Roles);
        }
    }
}
