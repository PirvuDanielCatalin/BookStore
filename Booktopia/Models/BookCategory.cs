using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Booktopia.Models
{
    public class BookCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int BookId { get; set; }
        public virtual Book book { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public virtual Category category { get; set; }

    }
}