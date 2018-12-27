using Booktopia.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Booktopia.Controllers
{
    [Authorize(Roles = "Colaborator,Administrator")]
    public class PartnerRequirementsController : Controller
    {
        private ApplicationDbContext db = ApplicationDbContext.Create();
        // GET: PartnerRequirements
        public ActionResult Index()
        {
            return View();
        }
        [NonAction]
        public JsonResult getAllRequirements()
        {
            var requirements = db.PartnerRequirements.Include("Book").Include("User");
            if (User.IsInRole("Colaborator"))
                requirements.Where(requirement => requirement.UserId == User.Identity.GetUserId());
            return Json(requirements.ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Show(int id)
        {
            PartnerRequirement requirement = db.PartnerRequirements.Find(id);
            ViewBag.afisareButoane = false;
            if (User.IsInRole("Administrator"))
            {
                ViewBag.afisareButoane = true;
            }
            ViewBag.esteAdmin = User.IsInRole("Administrator");
            return View(requirement);

        }

        public ActionResult New()
        {
            return View();
        }
        [HttpPost]
        public ActionResult New(PartnerRequirement requirement)
        {
            try
            {
                    if (ModelState.IsValid)
                    {
                        db.PartnerRequirements.Add(requirement);
                        db.SaveChanges();
                        TempData["message"] = "Cererea a fost inregistrata cu succes.Dupa aprobarea cererii de catre administrator, produsul va fi disponibil in magazin !";
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
            PartnerRequirement requirement = db.PartnerRequirements.Find(id);
            return View(requirement);
        }
        [HttpPut]
        public ActionResult Edit(int id, PartnerRequirement requestRequirement)
        {
            try
            {
                PartnerRequirement requirement = db.PartnerRequirements.Find(id);
                if (ModelState.IsValid)
                {
                    if ((User.Identity.GetUserId() == requirement.UserId) || (User.IsInRole("Administrator")))
                    {
                        if (TryUpdateModel(requirement))
                        {
                            requirement.Cantitate = requestRequirement.Cantitate;
                            if (User.IsInRole("Administrator"))
                            {
                                requirement.Status = requestRequirement.Status;
                            }
                            db.SaveChanges();
                        }
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["message"] = "Nu puteti edita cererea !";
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
        public ActionResult Delete(int id)
        {
            PartnerRequirement requirement = db.PartnerRequirements.Find(id);
            if ((requirement.Status == 0) && (User.Identity.GetUserId() == requirement.UserId) || User.IsInRole("Administrator"))
            {
                TempData["message"] = "Cererea a fost stersa !";
                db.PartnerRequirements.Remove(requirement);
                db.Books.Remove(db.Books.Find(requirement.BookId));
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = "Cererea nu poate fi stearsa !";
                return RedirectToAction("Index");
            }
        }
    }
}