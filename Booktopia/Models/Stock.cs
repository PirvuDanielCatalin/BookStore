using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Booktopia.Models
{
    public class Stock
    {
        [Key]
        public int StockId { get; set; }
        [Required]
        public int Cantitate { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public virtual Book book { get; set; }
    }
}