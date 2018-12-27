using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Booktopia.Models
{
    public class PartnerRequirement
    {
        [Key,Column(Order = 1)]
        public int IdCerere { get; set; }
        [Key, Column(Order = 2)]
        public int Cantitate { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public virtual Book book { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public virtual ApplicationUser User { get; set; }
    }
}