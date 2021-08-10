using Hotel.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.DAL.EF
{
    public class HotelInitializer
    {
        //Winter
        private const int winterStart = 11;
        private const int winterEnd = 4;
        //Summer
        private const int summerStart = 5;
        private const int summerEnd = 10;


        public static void Initialize(HotelContext context)
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (!context.Categories.Any())
                CategoryInitialize(context);

            if (!context.Prices.Any())
                PriceInitialize(context, context.Categories.OrderBy(c => c.Name).ToList());

            if (!context.Rooms.Any())
                RoomInitialize(context);

            if (!context.Guests.Any())
                GuestInitialize(context);

            if (!context.Bookings.Any())
                BookingInitialize(context);

            context.SaveChanges();
        }

        public static void PriceInitialize(HotelContext context, List<Category> categories)
        {
            int year = 2020;
            int startPrice = 111;
            //Default
            var defaultPrice = new Price
            { 
                Coast = 1000, Start = DateTime.MinValue, End = new DateTime(year, winterStart, 1) 
            };
            foreach (var cat in context.Categories)
                defaultPrice.Categories.Add(cat);

            Price price;
            DateTime start;
            DateTime end;
            for (int j = 0; j < 10; j++)
            {
                //Winter
                start = new DateTime(year, winterStart, 1);
                end = new DateTime(year + 1, winterEnd, DateTime.DaysInMonth(year, winterEnd));
                for (int i = 0; i < categories.Count; i++)
                {
                    price = new Price
                    { Coast = startPrice * (i + 1), Start = start, End = end };
                    price.Categories.Add(categories[i]);
                    context.Prices.Add(price);
                }

                //Summer
                year++;
                start = new DateTime(year, summerStart, 1);
                end = new DateTime(year, summerEnd, DateTime.DaysInMonth(year, summerEnd));
                for (int i = 0; i < categories.Count; i++)
                {
                    price = new Price
                    { Coast = startPrice * 2 * (i + 1), Start = start, End = end };
                    price.Categories.Add(categories[i]);
                    context.Prices.Add(price);
                }
            }

            defaultPrice.Start = new DateTime(year, summerEnd + 1, 1);
            defaultPrice.End = DateTime.MaxValue;
            foreach (var cat in context.Categories)
                defaultPrice.Categories.Add(cat);
            context.Prices.Add(defaultPrice);

            context.SaveChanges();
        }

        private static void CategoryInitialize(HotelContext context)
        {
            var categories = new List<Category>
            {
                new Category { Name = Categories.Single, Bed = 1, MaxPeople = 1},
                new Category { Name = Categories.Double, Bed = 2, MaxPeople = 2},
                new Category { Name = Categories.Triple, Bed = 3, MaxPeople = 3},
                new Category { Name = Categories.Family, Bed = 4, MaxPeople = 5},
                new Category { Name = Categories.President, Bed = 2, MaxPeople = 6},
                new Category { Name = Categories.Lux, Bed = 3, MaxPeople = 6}
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();
        }


        private static void RoomInitialize(HotelContext context)
        {
            Room room;
            int countRoomOnFloor = 3;
            var categories = context.Categories.OrderBy(c => c.Name).ToList();


            for (int i = 0; i < categories.Count; i++)
                for (int j = 0; j < countRoomOnFloor; j++)
                {
                    room = new Room { Name = i + 1 + "0" + j + "A", CategoryID = categories[i].ID };
                    context.Rooms.Add(room);
                }

            context.SaveChanges();
        }

        private static void GuestInitialize(HotelContext context)
        {
            var guest = new Guest
            {
                Name = "Andrew",
                Surname = "Andreev",
                Patronymic = "Admin",
                Email = "andew@gmail.com"
            };
            context.Guests.Add(guest);
            context.SaveChanges();
        }

        private static void BookingInitialize(HotelContext context)
        {
            var booking = new Booking
            {
                GuestID = context.Guests.First().ID,
                RoomID = context.Rooms.First().ID,
                CheckIn = new DateTime(2021, 8, 1),
                CkeckOut = new DateTime(2021, 8, 8)
            };
            context.Bookings.Add(booking);
            context.SaveChanges();
        }
    }
}
