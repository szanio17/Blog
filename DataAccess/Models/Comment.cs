using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataAccess.Interface;

namespace DataAccess.Models
{
    public class Comment : IEntity
    {
        public virtual int Id { get; set; }
        [Required(ErrorMessage = "Nick autora musí být zadán")]
        public virtual string AuthorNick { get; set; }
        [Required(ErrorMessage = "Email autora musí být zadán")]
        public virtual string AuthorEmail { get; set; }
        [AllowHtml]
        [Required(ErrorMessage = "Text příspěvku autora musí být zadán")]
        [StringLength(2000)]
        public virtual string Content { get; set; }
        public virtual string IpAddress { get; set; }
        public virtual string UserAgent { get; set; }
        public virtual Article Article { get; set; }
    }
}
