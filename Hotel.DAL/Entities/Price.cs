using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.DAL.Entities
{
    public class Price
    {        
        public int ID { set; get; }
        public int CategoryID { set; get; }
        [DataType(DataType.Currency)]
        public int Coast { set; get; }
        public DateTime Start { set; get; }
        public DateTime End { set; get; }
        public Category Category { set; get; }
    }
}
