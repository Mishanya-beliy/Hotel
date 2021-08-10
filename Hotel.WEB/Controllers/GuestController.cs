using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.WEB.Additional;
using Hotel.WEB.Models.Guest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Hotel.WEB.Controllers
{
    public class GuestController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGuestService _service;
        private readonly ILogger<GuestController> _logger;

        public GuestController(IGuestService service, IMapper mapper, ILogger<GuestController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        [Route(Routes.GuestList, Name = nameof(Routes.GuestList))]
        public IActionResult Index()
        {
            var guests = _mapper.Map<List<ShortGuest>>(_service.GetAllGuests());
            return View(guests);
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        [Route(Routes.GuestDetails, Name = nameof(Routes.GuestDetails))]
        public IActionResult Details(int id)
        {
            var guest = _mapper.Map<FullGuest>(_service.Get(id));
            if (guest is null)
                return Redirect(Routes.Home);

            return View(guest);
        }


        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        [Route(Routes.GuestCreate, Name = nameof(Routes.GuestCreate))]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        [Route(Routes.GuestCreate, Name = nameof(Routes.GuestCreate))]
        public IActionResult Create(FullGuest model)
        {
            if (ModelState.IsValid)
            {
                var id = _service.Create(_mapper.Map<GuestDTO>(model));
                if (id > 0)
                {
                    _logger.LogInformation("Create guest with id: " + id);
                    return RedirectToRoute(nameof(Routes.GuestDetails), id);
                }
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = Roles.User)]
        [Route(Routes.GuestEdit, Name = nameof(Routes.GuestEdit))]
        public IActionResult Edit()
        {
            var id = Additional.User.GetGuestIdFromClaims(HttpContext.User.Claims);
            if (id == -1)
                return Redirect(Routes.Home);

            var guest = _mapper.Map<FullGuest>(_service.Get(id));
            if (guest == null)
                return Redirect(Routes.Home);

            return View(guest);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.User)]
        [Route(Routes.GuestEdit, Name = nameof(Routes.GuestEdit))]
        public ActionResult Edit(FullGuest model)
        {
            var id = Additional.User.GetGuestIdFromClaims(HttpContext.User.Claims);
            if (id < 1)
                return Redirect(Routes.Home);

            if (ModelState.IsValid)
                try
                {
                    if(_service.Update(_mapper.Map<GuestDTO>(model))) 
                        _logger.LogInformation("Edit guest with id: " + id);
                    else
                        _logger.LogInformation("Failed edit guest with id: " + id);
                }
                catch
                {
                    return Redirect(Routes.Home);
                }

            return Redirect(Routes.GuestEdit);
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        [Route(Routes.GuestDelete, Name = nameof(Routes.GuestDelete))]
        public ActionResult Delete(int id)
        {
            if (_service.Remove(id))
            {
                _logger.LogInformation("Delete guest with id: " + id);
                return Redirect(Routes.GuestList);
            }

            _logger.LogInformation("Failed delete guest with id: " + id);
            return Redirect(Routes.Home);
        }
    }
}
