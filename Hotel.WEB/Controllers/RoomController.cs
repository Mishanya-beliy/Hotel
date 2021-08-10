using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.WEB.Additional;
using Hotel.WEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.WEB.Controllers
{
    public class RoomController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRoomService _service;
        private readonly ICategoryService _serviceCategory;

        public RoomController(IRoomService service, ICategoryService serviceCategory, IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
            _serviceCategory = serviceCategory;
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        [Route(Routes.RoomList, Name = nameof(Routes.RoomList))]
        public IActionResult Index()
        {
            var rooms = _mapper.Map<IEnumerable<RoomModel>>(_service.GetAllRooms());
            if (rooms is null)
                return Redirect(Routes.Home);

            return View(rooms);
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        [Route(Routes.RoomEdit, Name = nameof(Routes.RoomEdit))]
        public ActionResult Edit(int id)
        {
            var room = _mapper.Map<RoomModel>(_service.Get(id));
            if (room is null)
                return Redirect(Routes.Home);

            var categories = _serviceCategory.GetAllCategories();
            if (categories is null)
                return Redirect(Routes.RoomList);
            var first = categories.FirstOrDefault();
            ViewBag.Categories = new SelectList(categories, nameof(first.ID), nameof(first.Name));

            return View(room);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        [Route(Routes.RoomEdit, Name = nameof(Routes.RoomEdit))]
        public ActionResult Edit(RoomModel model)
        {
            if (ModelState.IsValid)
                if(_service.Update(_mapper.Map<RoomDTO>(model)))
                    return Redirect(Routes.RoomList);

            return Redirect(Routes.Home);
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        [Route(Routes.RoomCreate, Name = nameof(Routes.RoomCreate))]
        public ActionResult Create()
        {
            var categories = _serviceCategory.GetAllCategories();
            if (categories is null)
                return Redirect(Routes.RoomList);
            var first = categories.FirstOrDefault();
            ViewBag.Categories = new SelectList(categories, nameof(first.ID), nameof(first.Name));

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        [Route(Routes.RoomCreate, Name = nameof(Routes.RoomCreate))]
        public ActionResult Create(RoomModel model)
        {
            try
            {
                var id = _service.Create(_mapper.Map<RoomDTO>(model));
                if (id < 1)
                    return Redirect(Routes.Home);
            }
            catch
            {
                return Redirect(Routes.Home);
            }

            return Redirect(Routes.RoomList);
        }

        [HttpGet]
        [Route(Routes.RoomFind)]
        [Route(Routes.RoomFind, Name = nameof(Routes.RoomFind))]
        public IActionResult Find(RoomFind model)
        {
            if (ModelState.IsValid)
                return RedirectToAction("Display",
                        new { countPeople = model.CountPeople, start = model.From, end = model.To });

            return RedirectToRoute(nameof(Routes.Home), "#find");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route(Routes.RoomDisplay, Name = nameof(Routes.RoomDisplay))]
        public IActionResult Display(RoomFind model)
        {
            var rooms = _mapper.Map<IEnumerable<RoomModel>>(
                       _service.GetFreeRooms(model.From, model.To, model.CountPeople));

            if(rooms is null)
                return Redirect(Routes.Home);

            ViewBag.Start = model.From;
            ViewBag.End = model.To;
            return View(rooms);
        } 

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        [Route(Routes.RoomDelete, Name = nameof(Routes.RoomDelete))]
        public IActionResult Delete(int id)
        {
            if(_service.Delete(id))
                return Redirect(Routes.RoomList);

            return Redirect(Routes.Home);
        }       
    }
}
