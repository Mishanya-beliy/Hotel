using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.DAL.Entities;
using Hotel.WEB.Models;
using Hotel.WEB.Models.Booking;
using Hotel.WEB.Models.Guest;

namespace Hotel.WEB.Profiles
{
    public class HotelProfile : Profile
    {
        public HotelProfile()
        {
            #region Guest
            CreateMap<GuestModel, GuestDTO>();
            CreateMap<GuestDTO, Guest>();

            CreateMap<Guest, GuestDTO>();
            CreateMap<GuestDTO, GuestModel>();

            CreateMap<FullGuest, GuestDTO>();
            CreateMap<GuestDTO, FullGuest>();

            CreateMap<GuestDTO, ShortGuest>();
            CreateMap<CreateGuest, GuestDTO>();

            #endregion

            #region Booking
            CreateMap<Booking, BookingDTO>();
            CreateMap<BookingDTO, BookingModel>();

            CreateMap<BookingModel, BookingDTO>();
            CreateMap<BookingDTO, Booking>();

            CreateMap<BookingCreateModel, BookingDTO>();
            CreateMap<ConfirmBookingDTO, ConfirmBooking>();
            #endregion


            CreateMap<Room, RoomDTO>();
            CreateMap<RoomDTO, RoomModel>();

            CreateMap<RoomModel, RoomDTO>();
            CreateMap<RoomDTO, Room>();


            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, CategoryModel>();

            CreateMap<CategoryModel, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();


            CreateMap<Price, PriceDTO>();
            CreateMap<PriceDTO, PriceModel>();

            CreateMap<PriceModel, PriceDTO>();
            CreateMap<PriceDTO, Price>();
        }
    }
}
