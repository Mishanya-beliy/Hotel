using System;

namespace Hotel.API.Models
{
    public class PriceModel
    {
        public int ID { set; get; }
        public int CategoryID { set; get; }
        public int Coast { set; get; }
        public DateTime Start { set; get; }
        public DateTime End { set; get; }
        public CategoryModel Category { set; get; }
    }
}
