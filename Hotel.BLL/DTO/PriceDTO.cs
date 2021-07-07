using System;

namespace Hotel.BLL.DTO
{
    public class PriceDTO
    {
        public int ID { set; get; }
        public int CategoryID { set; get; }
        public int Coast { set; get; }
        public DateTime Start { set; get; }
        public DateTime End { set; get; }
        public CategoryDTO Category { set; get; }
    }
}
