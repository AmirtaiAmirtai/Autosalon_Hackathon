using харкатон.Models;

namespace харкатон.Helpers
{
    public class SeedData
    {
        public static List<Admin> Seed()
        {

            List<Admin> admins = new List<Admin>();
            var admin = new Admin
            {
                Loggin = "admin",
                Password = "password",
                Activate = false
            };
            admins.Add(admin);

            return admins;
        }
    }
}
