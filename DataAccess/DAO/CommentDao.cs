using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using NHibernate.Criterion;

namespace DataAccess.DAO
{
    public class CommentDao : DaoBase<Comment>
    {
        public CommentDao() : base()
        {
                
        }
        public IList<Comment> GetByArticleId(Article article)
        {
            return session.CreateCriteria<Comment>().Add(Restrictions.Eq("Article", article)).List<Comment>();
        }
    }
}
