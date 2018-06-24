using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using DataAccess.Interface;

namespace DataAccess.Models
{
    public class User : MembershipUser, IEntity
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Nick { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual Role Role { get; set; }
    }
}
