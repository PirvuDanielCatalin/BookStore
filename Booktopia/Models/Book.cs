using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        [Required]
        public int Status { get; set; }
        public int Id { get; set; }
        [Required]
        public virtual BookCategory bookCategory { get; set; }
        [Required]
        public IEnumerable<SelectListItem> BookCategories { get; set; }
        public int IdCerere { get; set; }
        public virtual PartnerRequirement PartenerRequirement { get; set; }
        public IEnumerable<SelectListItem> PartenerRequirements { get; set; }
        public int StockId { get; set; }
        public virtual Stock stock { get; set; }
        [Required]
        public int RatingId { get; set; }
        [Required]
        public virtual Rating rating { get; set; }
        public IEnumerable<SelectListItem> Ratings { get; set; }
        public int CommentId { get; set; }
        public virtual BookComment bookComment { get; set; }
        public IEnumerable<SelectListItem> BookComments { get; set; }
        public int BuyId { get; set; }
        public virtual Buy buy { get; set; }
        public IEnumerable<SelectListItem> Buys { get; set; }
    }
}