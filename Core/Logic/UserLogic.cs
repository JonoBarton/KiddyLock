using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using Core.DataBase;
using Core.Models;

namespace Core.Logic
{
    public static class UserLogic
    {
        public static List<User> GetUsers()
        {
            using (var ctx = new DataBaseContext())
            {
                return ctx.Users.ToList();
            }
        }

        public static User AddUser(User user)
        {
            using (var ctx = new DataBaseContext())
            {
                ctx.Users.Add(user);
                ctx.SaveChanges();
            }

            return user;
        }
    }
}
