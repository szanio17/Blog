using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataAccess.Interface;

namespace DataAccess.Models
{
    public class Article : IEntity
    {

        public virtual int Id { get; set; }
        [Required(ErrorMessage = "Titulek musí být zadán")]
        [StringLength(100)]
        public virtual string Title { get; set; }
        public virtual int AuthorId { get; set; }
        public virtual string AuthorName { get; set; }
        [Required(ErrorMessage = "Obsah musí být zadán")]
        [StringLength(4000)]
        [AllowHtml]
        public virtual string Content { get; set; }
    }
}
