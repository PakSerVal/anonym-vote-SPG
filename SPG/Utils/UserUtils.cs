using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPG.Data;
using SPG.Models.Db;

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
    }
}
