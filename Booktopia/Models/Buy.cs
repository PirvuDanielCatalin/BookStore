using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Booktopia.Models
{
    public class Buy
    {
        [Key]
        public int BuyId { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public virtual Book book { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public virtual ApplicationUser User { get; set; }

        public int InvoiceId { get; set; }
        public virtual Invoice invoice { get; set; }

    }
}