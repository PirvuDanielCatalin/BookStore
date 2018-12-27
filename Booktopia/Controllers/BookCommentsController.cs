using Booktopia.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Booktopia.Controllers
{
    public class BookCommentsController : Controller
    {
        private ApplicationDbContext db = ApplicationDbContext.Create();
        // GET: BookComments
        [Authorize(Roles = "User,Colaborator,Administrator")]
        public ActionResult New()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "User,Colaborator,Administrator")]
        public ActionResult New(BookComment bookComment)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (User.Identity.GetUserId() != bookComment.book.PartenerRequirement.UserId)
                    {
                        db.BookComments.Add(bookComment);
                        db.SaveChanges();
                        TempData["message"] = "Comentariul a fost adaugat!";
                    }
                    else
                    {
                        TempData["message"] = "Nu puteti lasa comentarii la propria carte !";
                    }
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
        public ActionResult Show(int id)
        {
            BookComment bookComment = db.BookComments.Find(id);
            return View(bookComment);
        }
        [Authorize(Roles = "User,Colaborator,Administrator")]
        public ActionResult Edit(int id)
        {
            BookComment bookComment = db.BookComments.Find(id);
            if (User.Identity.GetUserId() != bookComment.book.PartenerRequirement.UserId)
                return View(bookComment);
            else
            {
                TempData["message"] = "Nu puteti lasa comentarii la propria carte !";
                return RedirectToRoute("/home/index");
            }
        }
        [HttpPut]
        public ActionResult Edit(int id, BookComment requestBookComment)
        {
            try
            {
                BookComment bookComment = db.BookComments.Find(id);
                if (ModelState.IsValid)
                {
                    if (User.Identity.GetUserId() != bookComment.book.PartenerRequirement.UserId)
                    {
                        if (TryUpdateModel(bookComment))
                        {
                            bookComment.Comentariu = requestBookComment.Comentariu;
                            bookComment.DataAprobare = requestBookComment.DataAprobare;
                            db.SaveChanges();
                        }
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["message"] = "Nu puteti lasa comentarii la propria carte !";
                        return RedirectToRoute("/home/index");
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
        [Authorize(Roles = "User,Colaborator,Administrator")]
        public ActionResult Delete(int id)
        {
            BookComment bookComment = db.BookComments.Find(id);
            if (User.Identity.GetUserId() != bookComment.book.PartenerRequirement.UserId)
            {
                TempData["message"] = "Comentariul a fost sters !";
                db.BookComments.Remove(bookComment);
                db.SaveChanges();
            }
            else
                TempData["message"] = "Nu puteti lasa comentarii la propria carte !";
            return RedirectToRoute("/home/index");
        }
    }
}