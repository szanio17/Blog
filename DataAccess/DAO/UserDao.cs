using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using NHibernate.Criterion;

namespace DataAccess.DAO
{
    public class UserDao : DaoBase<User>
    {
        public User GetByLoginAndPassword(string nick, string password)
        {
            return session.CreateCriteria<User>()
                .Add(Restrictions.Eq("Nick", nick))
                .Add(Restrictions.Eq("Password", password))
                .UniqueResult<User>();

        }

        public User GetByLogin(string nick)
        {
            return session.CreateCriteria<User>()
                .Add(Restrictions.Eq("Nick", nick))
                .UniqueResult<User>();

        }
    }
}
