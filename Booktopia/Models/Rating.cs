using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Booktopia.Models
{
    public class Rating
    {
        [Key]
        public int RatingId { get; set; }
        [Required]
        public float RatingValue { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public virtual ApplicationUser User { get; set; }

        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public virtual Book book{ get; set; }
    }
}