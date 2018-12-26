using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Booktopia.Models
{
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }
        [Required]
        public DateTime data { get; set; }
        [Required]
        public string AdresaFacturare { get; set; }
        [Required]
        public string AdresaLivrare { get; set; }
        [Required]
        public int BuyId { get; set; }
        [Required]
        public virtual Buy buy { get; set; }
        public IEnumerable<SelectListItem> Buys { get; set; }
    }
}