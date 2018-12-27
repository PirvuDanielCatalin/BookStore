using Booktopia.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Booktopia.Controllers
{
    [Authorize(Roles = "User,Colaborator,Administrator")]
    public class RatingController : Controller
    {
        private ApplicationDbContext db = ApplicationDbContext.Create();
        // GET: Rating
        public ActionResult New()
        {
            return View();
        }
        [HttpPost]
        public ActionResult New(Rating rating)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (User.Identity.GetUserId() != rating.book.PartenerRequirement.UserId)
                    {
                        db.Ratings.Add(rating);
                        db.SaveChanges();
                        TempData["message"] = "Cartea a fost evaluata !";
                    }
                    else
                    {
                        TempData["message"] = "Nu puteti evalua propria carte !";
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
        public ActionResult Edit(int id)
        {
            Rating rating = db.Ratings.Find(id);
            if (User.Identity.GetUserId() != rating.book.PartenerRequirement.UserId)
                return View(rating);
            else
            {
                TempData["message"] = "Nu puteti evalua propria carte !";
                return RedirectToRoute("/home/index");
            }
        }
        [HttpPut]
        public ActionResult Edit(int id, Rating requestRating)
        {
            try
            {
                Rating rating = db.Ratings.Find(id);
                if (ModelState.IsValid)
                {
                    if (User.Identity.GetUserId() != rating.book.PartenerRequirement.UserId)
                    {
                        if (TryUpdateModel(rating))
                        {
                            rating.RatingValue = requestRating.RatingValue;
                            db.SaveChanges();
                        }
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["message"] = "Nu puteti evalua propria carte !";
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
        public ActionResult Delete(int id)
        {
            Rating rating = db.Ratings.Find(id);
            if (User.Identity.GetUserId() != rating.book.PartenerRequirement.UserId)
            {
                TempData["message"] = "Evaluarea a fost sters !";
                db.Ratings.Remove(rating);
                db.SaveChanges();
            }
            else
                TempData["message"] = "Nu puteti evalua propria carte !";
            return RedirectToRoute("/home/index");
        }
    }
}