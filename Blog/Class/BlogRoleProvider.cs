using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using DataAccess.DAO;
using DataAccess.Models;

namespace Blog.Class
{
    public class BlogRoleProvider : RoleProvider
    {
        public override bool IsUserInRole(string username, string roleName)
        {
            UserDao uDao = new UserDao();
            User user = uDao.GetByLogin(username);
            if (user == null)
                return false;

            return user.Role.Identificator == roleName;
        }

        public override string[] GetRolesForUser(string username)
        {
            UserDao uDao = new UserDao();
            User user = uDao.GetByLogin(username);
            if (user == null)
            {
                return new string[] { };
            }

            return new string[] { user.Role.Identificator };
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}