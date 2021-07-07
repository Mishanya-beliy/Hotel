using Hotel.DAL.Entities;
using Hotel.DAL.Repositories;
using System;

namespace Hotel.DAL.EF
{
    public class HotelInitializer
    {
        private static void CategoryInitializer(HotelContext context)
        {
            var category = new Category
            {
                Name = "Lux",
                Bed = 2
            };

            context.Categories.Add(category);
            context.SaveChanges();
        }
        private static void PriceInitializer(HotelContext context)
        {
            var price = new Price
            {
                CategoryID = 1,
                Coast = 444,
                Start = new DateTime(2021, 1, 1),
                End = new DateTime(2021, 12, 31)
            };
            context.Prices.Add(price);
            context.Categories.Find(1).Prices.Add(price);
            context.SaveChanges();
        }
        private static void RoomInitializer(HotelContext context)
        {
            var room = new Room
            {
                CategoryID = 1,
                Name = "101A",
            };

            context.Rooms.Add(room);
            context.SaveChanges();
        }

        private static void GuestInitializer(HotelContext context)
        {
            var guest = new Guest
            {
                Name = "Andrew",
                Surname = "Andreev",
                Patronymic = "Andreevich"
            };
            context.Guests.Add(guest);
            context.SaveChanges();
        }

        private static void BookingInitializer(HotelContext context)
        {
            var booking = new Booking
            {
                GuestID = 1,
                RoomID = 1,
                BookingDate = DateTime.Now,
                CheckIn = new DateTime(2021, 7, 1),
                CkeckOut = new DateTime(2021, 7, 3)
            };

            context.Bookings.Add(booking);
            context.SaveChanges();
        }

        public static void Initialize(HotelContext context)
        {
            context.Database.EnsureCreated();

            //if (context.Guests.Any)
            //{
            //    return;  
            //}

            CategoryInitializer(context);
            PriceInitializer(context);
            RoomInitializer(context);
            GuestInitializer(context);
            BookingInitializer(context);
        }
    }
}
