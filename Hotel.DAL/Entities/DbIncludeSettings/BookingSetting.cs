namespace Hotel.DAL.Entities.DbIncludeSettings
{
    public class BookingSetting
    {
        //Include
        public bool Guest { set; get; }
        public bool Room { set; get; }

        //ThenInclude
        public bool Category { set; get; }

        //Then then include
        public bool Price { set; get; } 
    }
}
