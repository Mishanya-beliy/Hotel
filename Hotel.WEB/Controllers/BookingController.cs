using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.WEB.Additional;
using Hotel.WEB.Models;
using Hotel.WEB.Models.Booking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.WEB.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _service;
        private readonly IGuestService _serviceGuest;
        private readonly IRoomService _serviceRoom;
        private readonly IPriceService _servicePrice;
        private readonly IMapper _mapper;

        public BookingController(IBookingService service, IGuestService serviceGuest,
            IRoomService serviceRoom, IMapper mapper, IPriceService servicePrice)
        {
            _service = service;
            _serviceGuest = serviceGuest;
            _serviceRoom = serviceRoom;
            _mapper = mapper;
            _servicePrice = servicePrice;
        }

        [HttpGet]
        [Route(Routes.BookingList)]
        [Authorize(Roles = Roles.Admin)]
        public ActionResult Index()
        {
            var bookings = _mapper.Map<IEnumerable<BookingModel>>(_service.GetAllBookings());
            return View(bookings);
        }

        [HttpGet]
        [Route(Routes.BookingDetails)]
        [Authorize(Roles = Roles.Admin)]
        public ActionResult Details(int id)
        {
            var booking = _mapper.Map<BookingModel>(_service.Get(id));
            if (booking is null)
                return Redirect(Routes.BookingList);

            return View(booking);
        }

        [HttpGet]
        [Authorize]
        [Route(Routes.Booking)]
        public ActionResult Booking(BookingCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var id = Additional.User.GetGuestIdFromClaims(HttpContext.User.Claims);
                if (id > 0)
                {
                    model.GuestID = id;
                    ConfirmBooking booking = _mapper.Map<ConfirmBooking>(
                        _service.Booking(_mapper.Map<BookingDTO>(model)));

                    if(booking != null)
                        return View(booking);
                }
            }          

            return Redirect(Routes.Home + "#find");
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        [Route(Routes.PriceProfit, Name = nameof(Routes.PriceProfit))]
        public ActionResult Profit()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        [Route(Routes.PriceProfit, Name = nameof(Routes.PriceProfit))]
        public ActionResult Profit(ProfitModel model)
        {
            model.Money = _servicePrice.Profit(model.Start, model.End);
            return View(model);
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route(Routes.BookingCreate)]
        [Authorize(Roles = Roles.Admin)]
        public ActionResult Create(BookingCreateModel collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        [Authorize]
        [Route(Routes.BookingEdit)]
        public ActionResult Edit(int id)
        {
            var booking = _mapper.Map<BookingModel>(_service.Get(id));
            if (booking is null)
                return Redirect(Routes.Home);

            var rooms = _mapper.Map<IEnumerable<RoomModel>>(_serviceRoom.GetAllRooms());
            if (rooms is null)
                return Redirect(Routes.Home);

            var guests = _mapper.Map<IEnumerable<GuestModel>>(_serviceGuest.GetAllGuests());
            if (guests is null)
                return Redirect(Routes.Home);

            var first = rooms.First();
            ViewBag.RoomID = new SelectList(rooms, nameof(first.ID), nameof(first.Name), first);
            var second = guests.First();
            ViewBag.GuestID = new SelectList(guests, nameof(second.ID), nameof(second.Name), second);


            return View(booking);
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [Route(Routes.BookingEdit)]
        public ActionResult Edit(int id, BookingModel model)
        {
            if (_service.Update(id, _mapper.Map<BookingDTO>(model)))
                return Redirect(Routes.BookingEdit);

            return Redirect(Routes.Home);
        }

        [HttpGet]
        [Route(Routes.BookingDelete)]
        [Authorize(Roles = Roles.Admin)]
        public ActionResult Delete(int id)
        {
            if(_service.Delete(id))
                return Redirect(Routes.BookingList);

            return Redirect(Routes.Home);
        }
    }
}
