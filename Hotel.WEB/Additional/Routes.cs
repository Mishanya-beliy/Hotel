namespace Hotel.WEB.Additional
{
    public static class Routes
    {
        public const string Home = "/";

        public const string Loging = "/Logs";
        public const string SetAdimin = "/SetAdmin";
        public const string RemoveAdmin = "/RemoveAdmin";

        #region Default
        private const string defaultList = "/List";
        private const string defaultCreate = "/Create";
        private const string defaultDetails = "/Details/";
        private const string defaultEdit = "/Edit";
        private const string defaultDelete = "/Delete/";
        #endregion

        #region Account
        private const string account = "/Account";
        public const string AccountLogin = account + "/Login";
        public const string AccountLogout = account + "/Logout";
        public const string AccountLockout = account + "/Lockout";
        public const string AccountRegister = account + "/Register";
        #endregion

        #region Guest
        private const string guest = "/Guest"; 
        public const string GuestEdit = guest + defaultEdit; 
        public const string GuestList = guest + defaultList; 
        public const string GuestDelete = guest + defaultDelete; 
        public const string GuestDetails = guest + defaultDetails; 
        public const string GuestCreate = guest + defaultCreate;
        #endregion

        #region Room
        private const string room = "/Room";
        public const string RoomFind = room + "Find";
        public const string RoomEdit = room + defaultEdit;
        public const string RoomList = room + defaultList;
        public const string RoomDelete = room + defaultDelete;
        public const string RoomDetails = room + defaultDetails;
        public const string RoomCreate = room + defaultCreate;
        public const string RoomDisplay = room + "/Display";
        #endregion

        #region Booking
        private const string booking = "/Booking";
        public const string Booking = booking + booking;
        public const string BookingEdit = booking + defaultEdit;
        public const string BookingList = booking + defaultList;
        public const string BookingDelete = booking + defaultDelete;
        public const string BookingDetails = booking + defaultDetails;
        public const string BookingCreate = booking + defaultCreate;
        #endregion

        #region Category
        private const string category = "/Category";
        public const string CategoryEdit = category + defaultEdit;
        public const string CategoryList = category + defaultList;
        public const string CategoryDelete = category + defaultDelete;
        public const string CategoryDetails = category + defaultDetails;
        public const string CategoryCreate = category + defaultCreate;
        #endregion

        #region Price
        private const string price = "/Price";
        public const string PriceEdit = price + defaultEdit;
        public const string PriceList = price + defaultList;
        public const string PriceDelete = price + defaultDelete;
        public const string PriceDetails = price + defaultDetails;
        public const string PriceCreate = price + defaultCreate;
        public const string PriceProfit = price + "/Profit";
        #endregion
    }
}
