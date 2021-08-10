using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.BLL.Services
{
    public class PriceService : IPriceService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _database;
        //private readonly IBookingService _bservice;
        private readonly ICategoryService _cservice;
        private readonly IRoomService _rservice;

        public PriceService(IRepositoryManager database, IMapper mapper, 
            //IBookingService bservice,
            ICategoryService cservice, IRoomService rservice)
        {
            _database = database;
            _mapper = mapper;
            //_bservice = bservice;
            _cservice = cservice;
            _rservice = rservice;
        }
        public IEnumerable<PriceDTO> GetAllPrices()
        {
            return _mapper.Map<List<PriceDTO>>(_database.Prices.GetAll());
        }
        public PriceDTO Get(int id)
        {
            return _mapper.Map<PriceDTO>(_database.Prices.Get(id));
        }

        private PriceDTO SelectPriceByDate(CategoryDTO category, DateTime date)
        {
            //foreach (PriceDTO price in GetAllPrices())
            //    if (price.Start < date && price.End > date && price. == category.ID)
            //        return price;
            return null;
        }

        public int CalculatePrice(CategoryDTO category, DateTime start, DateTime end)
        {
            int result = -1;
            if (category is null || category.Prices is null || !category.Prices.Any())
                return result;

            var prices = (from p in category.Prices
                         where p.Start <= end && p.End >= start
                         select p).OrderBy(p => p.Start).ToList();
            
            if (prices is null || prices.Count < 1)
                return result;

            result = 0;
            PriceDTO price = prices.First();
            for (DateTime current = start; current <= end; current = current.AddDays(1))
            {
                if (price.Start > current || price.End < current)
                    if (prices.Any())
                    {
                        foreach (var p in prices)
                            if (p.Start < current && p.End > current)
                            {
                                prices.Remove(price);
                                price = p;
                                break;
                            }
                    }
                    else
                        return result;
                result += price.Coast;
            }
            return result;
        }

        public int Profit(DateTime start, DateTime end)
        {
            if (start > end)
                return -1;

            int profit = 0;
            var bookings = _mapper.Map<IEnumerable<BookingDTO>>(_database.Bookings.GetAll().OrderBy(b => b.CheckIn));

            DateTime st;
            DateTime en;
            foreach (BookingDTO booking in bookings)
            {
                if ((booking.CheckIn >= start && booking.CheckIn <= end)
                    || (booking.CkeckOut >= start && booking.CkeckOut <= end))
                {
                    st = booking.CheckIn > start ? booking.CheckIn : start;
                    en = booking.CkeckOut < end ? booking.CkeckOut : end;
                    profit += CalculatePrice(booking.Room.Category, st, en);
                }
            }
            
            return profit;
        }

        public int ProfitPerMonth(DateTime date)
        {
            int profit = 0;
            var bookings = _database.Bookings.GetAll().OrderBy(b => b.CheckIn);
            foreach (BookingDTO booking in bookings)
            {
                if (booking.CheckIn.Month != date.Month)
                    if (booking.CkeckOut.Month != date.Month)
                        continue;
                    else
                    {
                        PriceDTO price = SelectPriceByDate(_cservice.Get(_rservice.Get(booking.RoomID).CategoryID), new DateTime(date.Year, date.Month, 1));
                        if (price.End > booking.CkeckOut)
                            profit += booking.CkeckOut.Day * price.Coast;
                        else
                        {
                            profit += price.End.Day * price.Coast;
                            while (price.End > booking.CkeckOut)
                            {
                                price = SelectPriceByDate(_cservice.Get(_rservice.Get(booking.RoomID).CategoryID), price.End.AddDays(1));
                                if (price.End < booking.CkeckOut)
                                    profit += (price.End.Day - price.Start.Day) * price.Coast;
                                else
                                    profit += (booking.CkeckOut.Day - price.Start.Day) * price.Coast;
                            }
                        }
                    }
                else
                {
                    PriceDTO price = SelectPriceByDate(_cservice.Get(_rservice.Get(booking.RoomID).CategoryID), booking.CheckIn);
                    if (price.End > booking.CkeckOut || price.End > new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month)))
                    {
                        int countDays = 0;
                        if (booking.CkeckOut > new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month)))
                            countDays = DateTime.DaysInMonth(date.Year, date.Month) - booking.CheckIn.Day;
                        else
                            countDays = booking.CkeckOut.Day - booking.CheckIn.Day;

                        profit += countDays * price.Coast;
                    }
                    else
                    {
                        DateTime end = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
                        if (end > booking.CkeckOut)
                            end = booking.CkeckOut;

                        profit += (price.End.Day - booking.CheckIn.Day) * price.Coast;
                        while (price.End < end)
                        {
                            price = SelectPriceByDate(_cservice.Get(_rservice.Get(booking.RoomID).CategoryID), price.End.AddDays(1));
                            if (price.End < end)
                                profit += (price.End.Day - price.Start.Day) * price.Coast;
                            else
                                profit += (end.Day - price.Start.Day) * price.Coast;
                        }
                    }
                }
            }
            return profit;
        }
    }
}
