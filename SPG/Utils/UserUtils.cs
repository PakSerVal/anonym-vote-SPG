using System;
using System.Text;
using System.Linq;
using SPG.Data;
using SPG.Models.Entities;
using System.Security.Cryptography;

namespace SPG.Utils
{
    public static class UserUtils
    {
        public static bool isAdmin(ElectContext electContext, int userId)
        {
            User user = electContext.Users.FirstOrDefault(u => u.ID == userId);
            if (user.Role == UserRole.admin)
            {
                return true;
            }
            return false;
        }

        public static string getSalt(int length = 32)
        {
            byte[] randomArray = new byte[length];
            string randomString;
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomArray);
            randomString = Convert.ToBase64String(randomArray);
            return randomString;
        }

        public static string getPasswordHash(string password, string salt)
        {
            var provider = new SHA1CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(String.Concat(password, salt));
            return Convert.ToBase64String(provider.ComputeHash(bytes));
        }
    }
}
