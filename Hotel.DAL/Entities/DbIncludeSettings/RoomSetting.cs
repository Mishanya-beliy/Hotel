namespace Hotel.DAL.Entities.DbIncludeSettings
{
    public class RoomSetting
    {
        //Include
        public bool Category { set; get; }
        public bool Booking { set; get; }

        //ThenInclude
        public bool Price { set; get; }
    }
}
