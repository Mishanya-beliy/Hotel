using System.Collections.Generic;

namespace Hotel.DAL.Entities
{
    public class Category
    {
        public int ID { set; get; }

        public Categories Name { set; get; }
        public int Bed { set; get; }
        public int MaxPeople { set; get; }

        public virtual ICollection<Price> Prices { set; get; }
    }
    public enum Categories
    {
        Single,
        Double,
        Triple,
        Family,
        Lux,
        President
    }
}
