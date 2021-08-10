namespace Auth.Models.EF
{
    public class HotelAuthInitializer
    {
        public static void Initialize(HotelAuthContext context)
        {
            context.Database.EnsureCreated();

            var account = new AccountModel
            {
                Login = "admin",
                Password = "1111"
            };

            context.Accounts.Add(account);
            context.SaveChanges();
        }
    }
}
