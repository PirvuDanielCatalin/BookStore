using Booktopia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Booktopia.Controllers
{

    
    public class CategoriesController : Controller
    {
        private ApplicationDbContext db = ApplicationDbContext.Create();
        // GET: Categories
        [Authorize(Roles = "User,Colaborator,Administrator")]
        public ActionResult Index()
        {
            return View();
        }
        [NonAction]
        [Authorize(Roles = "User,Colaborator,Administrator")]
        public JsonResult getAllCategories()
        {
            var categories = db.Categories.Include("BookCategory");
            return Json(categories.ToList(), JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "User,Colaborator,Administrator")]
        public ActionResult Show(int id)
        {
            Category category = db.Categories.Find(id);
            ViewBag.afisareButoane = false;
            if (User.IsInRole("Administrator"))
            {
                ViewBag.afisareButoane = true;
            }
            ViewBag.esteAdmin = User.IsInRole("Administrator");
            return View(category);

        }

        [Authorize(Roles = "Administrator")]
        public ActionResult New()
        { 
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult New(Category category)
        {
            try
            {
                if (User.IsInRole("Administrator"))
                {
                    if (ModelState.IsValid)
                    {
                        db.Categories.Add(category);
                        db.SaveChanges();
                        TempData["message"] = "Categoria a fost adaugata !";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.Clear();
                        return View();
                    }
                }
                else
                {
                    TempData["message"] = "Doar administratorul pot face asta !";
                    return RedirectToAction("Index");

                }
            }
            catch (Exception e)
            {
                return View();
            }
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            Category category = db.Categories.Find(id);
            if (User.IsInRole("Administrator"))
            {
                return View(category);
            }
            else
            {
                TempData["message"] = "Doar administratorul poate face asta !";
                return RedirectToAction("Index");
            }
        }
        [HttpPut]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id, Category requestCategory)
        {
            try
            {
                Category category = db.Categories.Find(id);
                if (ModelState.IsValid)
                {
                    if (User.IsInRole("Administrator"))
                    {
                        if (TryUpdateModel(category))
                        {
                            category.Nume = requestCategory.Nume;
                            db.SaveChanges();
                        }
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["message"] = "Doar administratorul poate face asta !";
                        return RedirectToAction("Index");
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
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            Category category = db.Categories.Find(id);
            TempData["message"] = "Categoria " + category.Nume + " a fost stersa !";
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}