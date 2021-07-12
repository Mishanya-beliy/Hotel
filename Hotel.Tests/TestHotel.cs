using AutoMapper;
using Hotel.API;
using Hotel.API.Controllers;
using Hotel.API.Models;
using Hotel.BLL.Interfaces;
using Hotel.DAL.Interfaces;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Tests
{
    public class TestHotel
    {
        private IWebHost server;
        [SetUp]
        public void Setup()
        {
            Startup startup = null;
            IServiceCollection serviceCollection = null;
            server = WebHost
                .CreateDefaultBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.Sources.Clear();
                    config.AddConfiguration(hostingContext.Configuration);
                    config.AddJsonFile("appsettings.json");
                    startup = new Startup(config.Build());
                })
                .ConfigureServices(sc =>
                {
                    startup.ConfigureServices(sc);
                    serviceCollection = sc;
                })
                .UseStartup<TestSturtup>()
                .Build();
            serviceCollection.BuildServiceProvider(new ServiceProviderOptions()
            {
                ValidateOnBuild = true,
                ValidateScopes = true
            });
        }

        [Test]
        public async Task TestGuestNotNull()
        {
            GuestController con = new GuestController(server.Services.GetRequiredService<IGuestService>(), server.Services.GetRequiredService<IMapper>());
            var response = con.Get();

            Assert.IsNotNull(response);
        }
        [Test]
        public async Task TestGuestGetType()
        {            
            GuestController con = new GuestController(server.Services.GetRequiredService<IGuestService>(), server.Services.GetRequiredService<IMapper>());
            var response = con.Get();
            Assert.IsInstanceOf<IEnumerable<GuestModel>>(response);
        }
        [Test]
        public async Task TestGuestGetResult()
        {
            GuestController con = new GuestController(server.Services.GetRequiredService<IGuestService>(), server.Services.GetRequiredService<IMapper>());
            var response = con.Get().ToList();

            IRepositoryManager repMan = server.Services.GetRequiredService<IRepositoryManager>();
            Assert.AreEqual(response.Count, repMan.Guests.GetAll().Count());
        }
        [Test]
        public async Task TestGuestRegistration()
        {
            GuestController con = new GuestController(server.Services.GetRequiredService<IGuestService>(), server.Services.GetRequiredService<IMapper>());
            GuestModel testGuest = new GuestModel
            {
                Name = "Ivan",
                Surname = "Ivanovich"
            };
            var before = con.Get().Count();
            con.Post(testGuest);
            var after = con.Get().Count();

            Assert.AreEqual(++before, after);
        }
        [Test]
        public async Task TestRoomNotNull()
        {
            RoomController con = new RoomController(server.Services.GetRequiredService<IRoomService>(), server.Services.GetRequiredService<IMapper>());
            var response = con.Get();

            Assert.IsNotNull(response);
        }
        [Test]
        public async Task TestRoomGetType()
        {
            RoomController con = new RoomController(server.Services.GetRequiredService<IRoomService>(), server.Services.GetRequiredService<IMapper>());
            var response = con.Get();
            Assert.IsInstanceOf<IEnumerable<RoomModel>>(response);
        }
        [Test]
        public async Task TestRoomGetResult()
        {
            RoomController con = new RoomController(server.Services.GetRequiredService<IRoomService>(), server.Services.GetRequiredService<IMapper>());
            var response = con.Get().ToList();

            IRepositoryManager repMan = server.Services.GetRequiredService<IRepositoryManager>();
            Assert.AreEqual(response.Count, repMan.Rooms.GetAll().Count());
        }


        [Test]
        public async Task TestRoomFreeNotNull()
        {
            RoomController con = new RoomController(server.Services.GetRequiredService<IRoomService>(), server.Services.GetRequiredService<IMapper>());
            var response = con.FreeRoom("2021-02-02");

            Assert.IsNotNull(response);
        }

        [Test]
        public async Task TestRoomFreeType()
        {
            RoomController con = new RoomController(server.Services.GetRequiredService<IRoomService>(), server.Services.GetRequiredService<IMapper>());
            var response = con.FreeRoom("2021-02-02");
            Assert.IsInstanceOf<IEnumerable<RoomModel>>(response);
        }
        [Test]
        public async Task TestRoomFreeResult()
        {
            RoomController con = new RoomController(server.Services.GetRequiredService<IRoomService>(), server.Services.GetRequiredService<IMapper>());
            var response = con.FreeRoom("2021-02-02").ToList();

            Assert.AreEqual(response.Count, 1);
        }
        [Test]
        public async Task TestBooking()
        {
            BookingController con = new BookingController(server.Services.GetRequiredService<IBookingService>(), server.Services.GetRequiredService<IMapper>());
            BookingModel testBooking = new BookingModel
            {
                GuestID = 1,
                RoomID = 1,
                BookingDate = DateTime.Now,
                CheckIn = new DateTime(2021, 3, 1),
                CkeckOut = new DateTime(2021, 3, 3)
            };
            var before = con.Get().Count();
            con.Post(testBooking);
            var after = con.Get().Count();

            Assert.AreEqual(++before, after);
        }

        [Test]
        public async Task TestPriceNotNull()
        {
            PriceController con = new PriceController(server.Services.GetRequiredService<IPriceService>(), server.Services.GetRequiredService<IMapper>());
            var response = con.Get("2021-07-02");

            Assert.IsNotNull(response);
        }

        [Test]
        public async Task TestPriceType()
        {
            PriceController con = new PriceController(server.Services.GetRequiredService<IPriceService>(), server.Services.GetRequiredService<IMapper>());
            var response = con.Get("2021-07-02");
            Assert.IsInstanceOf<int>(response);
        }
        [Test]
        public async Task TestPriceResult()
        {
            PriceController con = new PriceController(server.Services.GetRequiredService<IPriceService>(), server.Services.GetRequiredService<IMapper>());
            var response = con.Get("2021-07-02");

            Assert.AreEqual(response, 888);
        }
    }
}