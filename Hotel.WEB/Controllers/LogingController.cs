using Hotel.WEB.Additional;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.WEB.Controllers
{
    public class LogingController : Controller
    {
        [HttpGet]
        [Route(Routes.Loging)]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Index()
        {
            var logs = FileLogger.ReadLog();
            return View(logs);
        }
    }
}
