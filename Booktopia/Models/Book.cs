using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Booktopia.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        public string Titlu { get; set; }
        [Required]
        public string Autor { get; set; }
        [Required]
        public string Editura { get; set; }
        [Required]
        public string Descriere { get; set; }
        public string Fotografie { get; set; }
        [Required]
        public int Pret { get; set; }
    }
}