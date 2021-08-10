using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hotel.DAL.Entities
{
    public class Price
    {        
        public int ID { set; get; }
        [DataType(DataType.Currency)]
        public int Coast { set; get; }
        public DateTime Start { set; get; }
        public DateTime End { set; get; }
        public virtual ICollection<Category> Categories { set; get; } = new List<Category>();
    }
}
