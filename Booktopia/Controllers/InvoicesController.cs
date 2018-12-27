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
    public class InvoicesController : Controller
    {
        private ApplicationDbContext db = ApplicationDbContext.Create();
        // GET: Invoices
        public ActionResult Index()
        {
            return View();
        }
        [NonAction]
        public JsonResult getAllInvoices()
        {
            var invoices = db.Invoices.Include("Buy");
            if (!User.IsInRole("Administrator"))
                invoices.Where(invoice => invoice.buy.UserId == User.Identity.GetUserId());
            return Json(invoices.ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Show(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            ViewBag.afisareButoane = false;
            if (User.IsInRole("Administrator"))
            {
                ViewBag.afisareButoane = true;
            }
            ViewBag.esteAdmin = User.IsInRole("Administrator");
            if (invoice.buy.UserId == User.Identity.GetUserId() || User.IsInRole("Administrator"))
                return View(invoice);
            else
            {
                TempData["message"] = "Nu puteti vedea facturile altora !";
                return RedirectToAction("Index");
            }

        }

        public ActionResult New()
        {
            return View();
        }
        [HttpPost]
        public ActionResult New(Invoice invoice)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Invoices.Add(invoice);
                    db.SaveChanges();
                    TempData["message"] = "Factura a fost adaugata !";
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
            Invoice invoice = db.Invoices.Find(id);
            if (invoice.buy.UserId == User.Identity.GetUserId() || User.IsInRole("Administrator"))
                return View(invoice);
            else
            {
                TempData["message"] = "Nu puteti edita facturile altora !";
                return RedirectToAction("Index");
            }
        }
        [HttpPut]
        public ActionResult Edit(int id, Invoice requestInvoice)
        {
            try
            {
                Invoice invoice = db.Invoices.Find(id);
                if (ModelState.IsValid)
                {
                    if (requestInvoice.buy.UserId == User.Identity.GetUserId() || User.IsInRole("Administrator"))
                    {
                        if (TryUpdateModel(invoice))
                        {
                            invoice.AdresaFacturare = requestInvoice.AdresaFacturare;
                            invoice.AdresaLivrare = requestInvoice.AdresaLivrare;
                            invoice.data = requestInvoice.data;
                            db.SaveChanges();
                        }
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["message"] = "Nu puteti edita facturile altora !";
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
            Invoice invoice = db.Invoices.Find(id);
            if (invoice.buy.UserId == User.Identity.GetUserId() || User.IsInRole("Administrator"))
            { 
                TempData["message"] = "Factura a fost stersa !";
                db.Invoices.Remove(invoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = "Factura nu poate fi stearsa !";
                return RedirectToAction("Index");
            }
        }
    }
}