using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Booktopia.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        
        public int ProfileId { get; set; }
        public virtual Profile userProfile { get; set; }
        public int IdCerere { get; set; }
        public virtual PartnerRequirement PartenerRequirement { get; set; }
        public IEnumerable<SelectListItem> PartenerRequirements { get; set; }
        public int CommentId { get; set; }
        public virtual BookComment bookComment { get; set; }
        public IEnumerable<SelectListItem> BookComments { get; set; }
        public int RaitingId { get; set; }
        public virtual Rating rating { get; set; }
        public IEnumerable<SelectListItem> Ratings { get; set; }
        public int BuyId { get; set; }
        public virtual Buy buy { get; set; }
        public IEnumerable<SelectListItem> Buys { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookComment> BookComments { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Buy> Buys { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<PartnerRequirement> PartnerRequirements { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        
    }
}