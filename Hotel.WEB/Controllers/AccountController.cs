using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.DAL.EF;
using Hotel.WEB.Additional;
using Hotel.WEB.Models;
using Hotel.WEB.Models.Guest;
using Hotel.WEB.Models.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.WEB.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<GuestIdentity> _userManager;
        private readonly SignInManager<GuestIdentity> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IGuestService _guestService;
        private readonly IMapper _mapper;

        public AccountController(SignInManager<GuestIdentity> signInManager,
            ILogger<AccountController> logger, IGuestService guestService,
            IMapper mapper, UserManager<GuestIdentity> userManager)
        {
            _signInManager = signInManager;
            _guestService = guestService;
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
        }

        private async Task<GuestIdentity> GetUser()
        {
            var id = Additional.User.GetUserIdFromClaims(HttpContext.User.Claims);
            if (id == null)
                return null;

            return await _userManager.FindByIdAsync(id);
        }

        [HttpGet]
        [Authorize]
        [Route(Routes.SetAdimin)]
        public async Task<IActionResult> SetAdmin()
        {
            var user = GetUser().Result;
            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, Roles.Admin);
                await _signInManager.SignInAsync(user, false);
            }

            return Redirect(Routes.Home);
        }

        [HttpGet]
        [Route(Routes.RemoveAdmin)]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> RemoveAdmin()
        {
            var user = GetUser().Result;
            if (user != null)
            {
                await _userManager.RemoveFromRoleAsync(user, Roles.Admin);
                await _signInManager.SignInAsync(user, false);
            }

            return Redirect(Routes.Home);
        }

        [HttpGet]
        [Route(Routes.AccountLogin)]
        [AllowAnonymous]
        public IActionResult Login()
        {
            var some = HttpContext.User.Claims.ToList();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(Routes.AccountLogin, Name = nameof(Routes.AccountLogin))]
        public async Task<IActionResult> Login(AccountModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return Redirect(Routes.Home);
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return Redirect(Routes.AccountLockout);
                }
                else
                {
                    _logger.LogWarning("Invalid login attempt.");
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route(Routes.AccountRegister)]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(Routes.AccountRegister)]
        public async Task<IActionResult> Register(AccountRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                int id = _guestService.Create(_mapper.Map<GuestDTO>(new CreateGuest { Email = model.Email }));
                if (id < 1)
                    return View(model);

                var user = new GuestIdentity { UserName = model.Email, Email = model.Email, GuestID = id };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.User);
                    var resultSignIn = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, lockoutOnFailure: false);

                    if (resultSignIn.Succeeded)
                    {
                        _logger.LogInformation("User created a new account with password.");
                        return Redirect(Routes.Home);
                    }

                    return View(model);
                }
                foreach (var error in result.Errors)
                {
                    _logger.LogInformation(error.Description);
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }


        [HttpGet]
        [Route(Routes.AccountLogout)]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            return Redirect(Routes.Home);
        }
    }
}
