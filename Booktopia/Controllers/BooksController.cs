using Booktopia.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Booktopia.Controllers
{
    public class BooksController : Controller
    {
        private ApplicationDbContext db = ApplicationDbContext.Create();
        // GET: Books
        public ActionResult Index()
        {
            return View();
        }
        [NonAction]
        public JsonResult getAllBooks()
        {
            var books = db.Books.Include("BookCategory").Include("PartnerRequirement")
                          .Include("Rating").Include("BookComment").Include("Buy");
            if (!User.IsInRole("Administrator"))
            {
                var bookS = books.Where(book => book.Status == 1);
                return Json(bookS.ToList(), JsonRequestBehavior.AllowGet);
            }
            return Json(books.ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Show(int id)
        {
            Book book = db.Books.Find(id);
            ViewBag.afisareButoane = false;
            if (User.IsInRole("Administrator"))
            {
                ViewBag.afisareButoane = true;
            }
            ViewBag.esteAdmin = User.IsInRole("Administrator");
            return View(book);

        }
        [Authorize(Roles = "Colaborator,Administrator")]
        public ActionResult New()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Colaborator,Administrator")]
        public ActionResult New(Book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Books.Add(book);
                    db.SaveChanges();
                    if (User.IsInRole("Colaborator"))
                    {
                        PartnerRequirement requirement = new PartnerRequirement();
                        requirement.Cantitate = 0;
                        requirement.Status = 0;
                        requirement.BookId = book.BookId;
                        requirement.book = book;
                        requirement.UserId = User.Identity.GetUserId();
                        requirement.User = db.Users.Find(User.Identity.GetUserId());
                    }
                    TempData["message"] = "Cartea a fost adaugata cu succes !";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.Clear();
                    return View();
                }
            }
            catch (Exception e)
            {
                return View();
            }
        }
        [Authorize(Roles = "Colaborator,Administrator")]
        public ActionResult Edit(int id)
        {
            Book book = db.Books.Find(id);
            return View(book);
        }
        [HttpPut]
        [Authorize(Roles = "Colaborator,Administrator")]
        public ActionResult Edit(int id, Book requestBook)
        {
            try
            {
                Book book = db.Books.Find(id);
                if (ModelState.IsValid)
                {
                    if ((User.Identity.GetUserId() == requestBook.PartenerRequirement.UserId) || (User.IsInRole("Administrator")))
                    {
                        if (TryUpdateModel(book))
                        {
                            book.Autor = requestBook.Autor;
                            book.Descriere = requestBook.Descriere;
                            book.Editura = requestBook.Descriere;
                            book.Fotografie = requestBook.Fotografie;
                            book.Pret = requestBook.Pret;
                            if (User.IsInRole("Colaborator"))
                                book.Status = requestBook.Status;
                            book.Titlu = requestBook.Titlu;
                            book.BookCategories = requestBook.BookCategories;
                            book.bookCategory = requestBook.bookCategory;
                            book.BookComments = requestBook.BookComments;
                            book.bookComment = requestBook.bookComment;
                            book.Buys = requestBook.Buys;
                            book.BuyId = requestBook.BuyId;
                            book.CommentId = requestBook.BookId;
                            book.buy = requestBook.buy;
                            book.Id = requestBook.Id;
                            book.IdCerere = requestBook.IdCerere;
                            book.PartenerRequirement = requestBook.PartenerRequirement;
                            book.PartenerRequirements = requestBook.PartenerRequirements;
                            book.RatingId = requestBook.RatingId;
                            book.rating = requestBook.rating;
                            book.Ratings = requestBook.Ratings;
                            book.stock = requestBook.stock;
                            book.StockId = requestBook.StockId;
                            db.SaveChanges();
                        }
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["message"] = "Nu puteti edita cartea !";
                        return View();
                    }
                }
                else
                {
                    ModelState.Clear();
                    return View();
                }
            }
            catch (Exception e)
            {
                return View();
            }
        }
        [HttpDelete]
        [Authorize(Roles = "Colaborator,Administrator")]
        public ActionResult Delete(int id)
        {
            Book book = db.Books.Find(id);
            if ((book.Status == 1) && (User.Identity.GetUserId() == book.PartenerRequirement.UserId) || User.IsInRole("Administrator"))
            {
                TempData["message"] = "Cartea a fost stersa !";
                db.Books.Remove(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = "Cartea nu poate fi stearsa !";
                return RedirectToAction("Index");
            }
        }
    }
}