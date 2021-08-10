using Hotel.BLL.DTO;
using System;
using System.Collections.Generic;

namespace Hotel.BLL.Interfaces
{
    public interface IPriceService
    {
        IEnumerable<PriceDTO> GetAllPrices();
        PriceDTO Get(int id);
        public int ProfitPerMonth(DateTime date);
        public int CalculatePrice(CategoryDTO category, DateTime start, DateTime end);
        public int Profit(DateTime start, DateTime end);
    }
}
