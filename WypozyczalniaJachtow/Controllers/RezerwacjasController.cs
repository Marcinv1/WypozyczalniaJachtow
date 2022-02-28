using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WypozyczalniaJachtow.Models;

namespace WypozyczalniaJachtow.Controllers
{
    public class RezerwacjasController : Controller
    {
        private WypozyczalniaJachtowEntities1 db = new WypozyczalniaJachtowEntities1();

        // GET: Rezerwacjas
        public ActionResult Index()
        {
            var rezerwacja = db.Rezerwacja.Include(r => r.Jachty);
            return View(rezerwacja.ToList());
        }

        // GET: Rezerwacjas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezerwacja rezerwacja = db.Rezerwacja.Find(id);
            if (rezerwacja == null)
            {
                return HttpNotFound();
            }
            return View(rezerwacja);
        }

        // GET: Rezerwacjas/Create
        public ActionResult Create()
        {
            ViewBag.JachtIDs = new SelectList(db.Jachty, "ID", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,JachtID,Imie,NrTel,DataOd,DataDo,Zaliczka")] Rezerwacja rezerwacja)
        {
            if (ModelState.IsValid)
            {
                var query = db.Rezerwacja.Where(x =>
                    (rezerwacja.JachtID == x.JachtID) &&
                    (
                        (rezerwacja.DataOd > x.DataOd && rezerwacja.DataOd < x.DataDo) ||
                        (rezerwacja.DataDo > x.DataOd && rezerwacja.DataDo < x.DataDo)
                    )
                );

                var a = query.ToList();

                ViewBag.JachtIDs = new SelectList(db.Jachty, "ID", "Name");

                if (rezerwacja.DataDo < rezerwacja.DataOd || rezerwacja.DataOd < DateTime.Today)
                {
                        ModelState.AddModelError("Error", "Nie można zarezerwować z datą wsteczną.");
                        return View(rezerwacja);
                }

                if (a.Count > 0)
                {
                    ModelState.AddModelError("Error", "Terminy zajęte:");
                    foreach(var b in a)
                    {
                        ModelState.AddModelError("Error", b.DataOd.Value.ToString("dd.MM.yyyy") + " - " + b.DataDo.Value.ToString("dd.MM.yyyy"));
                    }
                    return View(rezerwacja);
                }
 
                db.Rezerwacja.Add(rezerwacja);
                db.SaveChanges();
                return RedirectToAction("Index");
 
            }
            return View(rezerwacja);
           
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezerwacja rezerwacja = db.Rezerwacja.Find(id);
            if (rezerwacja == null)
            {
                return HttpNotFound();
            }
            ViewBag.JachtID = new SelectList(db.Jachty, "ID", "Name", rezerwacja.JachtID);
            return View(rezerwacja);
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,JachtID,Imie,NrTel,DataOd,DataDo,Zaliczka")] Rezerwacja rezerwacja)
        {
            if (ModelState.IsValid)
            {

                if (rezerwacja.DataDo < rezerwacja.DataOd)
                {
                    ModelState.AddModelError("Error", "Nie można zarezerwować z datą wsteczną.");
                }
                db.Entry(rezerwacja).State = EntityState.Modified;


                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JachtID = new SelectList(db.Jachty, "ID", "Name", rezerwacja.JachtID);
            return View(rezerwacja);
        }

        // GET: Rezerwacjas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezerwacja rezerwacja = db.Rezerwacja.Find(id);
            if (rezerwacja == null)
            {
                return HttpNotFound();
            }
            return View(rezerwacja);
        }

        // POST: Rezerwacjas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rezerwacja rezerwacja = db.Rezerwacja.Find(id);
            db.Rezerwacja.Remove(rezerwacja);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
