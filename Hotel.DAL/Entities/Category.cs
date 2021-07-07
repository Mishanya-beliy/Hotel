using System.Collections.Generic;

namespace Hotel.DAL.Entities
{
    public class Category
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public int Bed { set; get; }
        public  ICollection<Price> Prices { set; get; }
    }
}
