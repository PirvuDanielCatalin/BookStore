using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Booktopia.Models
{
    public class BookComment
    {
        [Key]
        public int CommentId { get; set; }
        [Required]
        public string Comentariu { get; set; }
        [Required]
        public DateTime DataAprobare { get; set; }
        public string UserId { get; set; }
        [Required]
        public virtual ApplicationUser User { get; set; }
        public int BookId { get; set; }
        public virtual Book book { get; set; }

    }
}