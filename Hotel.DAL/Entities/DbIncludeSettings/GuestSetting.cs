namespace Hotel.DAL.Entities.DbIncludeSettings
{
    public class GuestSetting
    {
        //Include
        public bool Booking { set; get; }

        //ThenInclude
        public bool Room { set; get; }
    }
}
