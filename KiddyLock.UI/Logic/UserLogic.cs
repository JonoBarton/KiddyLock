using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Security.Principal;
using System.Threading.Tasks;
using Core.Models;
using KiddyLock.UI.Helpers;

namespace KiddyLock.UI.Logic
{
    public static class UserLogic
    {
        public static List<User> GetOsUsers()
        {
            var users = new List<User>();
            var builtinAdminSid = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
            var builtinUsersSid = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);

            var ctx = new PrincipalContext(ContextType.Machine);

            var adminGroup = GroupPrincipal.FindByIdentity(ctx, builtinAdminSid.Value);
            var userGroup = GroupPrincipal.FindByIdentity(ctx, builtinUsersSid.Value);

            if (adminGroup != null)
                foreach (var p in adminGroup.Members)
                {
                    users.Add(new User
                    {
                        Sid = p.Sid.ToString(),
                        Name = p.Name,
                        Guid = p.Guid == Guid.Empty || p.Guid == null ? Guid.Empty : (Guid)p.Guid,
                        DisplayName = p.DisplayName
                    });
                }

            if (userGroup == null) return users;
            foreach (var p in userGroup.Members)
            {
                users.Add(new User
                {
                    Sid = p.Sid.ToString(),
                    Name = p.Name,
                    Guid = p.Guid == Guid.Empty || p.Guid == null ? Guid.Empty : (Guid)p.Guid,
                    DisplayName = p.DisplayName
                });
            }
            return users;
           
        }

        public static async Task<List<User>> GetUsers()
        {
            var apiHelper = new ApiHelper();
            var users = await apiHelper.Get<List<User>>($"{ConfigurationManager.AppSettings["apiUrl"]}users");
            return users;
        }

        public static User AddUser(User user)
        {
            return null;
        }
    }
}
