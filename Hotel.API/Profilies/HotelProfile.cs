using AutoMapper;
using Hotel.API.Models;
using Hotel.BLL.DTO;
using Hotel.DAL.Entities;

namespace Hotel.API.Profilies
{
    public class HotelProfile : Profile
    {
        public HotelProfile()
        {
            CreateMap<Guest, GuestDTO>();
            CreateMap<GuestDTO, GuestModel>();

            CreateMap<GuestModel, GuestDTO>();
            CreateMap<GuestDTO, Guest>();


            CreateMap<Booking, BookingDTO>();
                     CreateMap<BookingDTO, BookingModel>();

            CreateMap<BookingModel, BookingDTO>();
            CreateMap<BookingDTO, Booking>();


            CreateMap<Room, RoomDTO>();
            CreateMap<RoomDTO, RoomModel>();

            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, CategoryModel>();

            CreateMap<Price, PriceDTO>();
            CreateMap<PriceDTO, PriceModel>();
        }
    }
}
