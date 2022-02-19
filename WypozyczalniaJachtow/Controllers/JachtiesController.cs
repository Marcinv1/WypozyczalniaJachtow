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
    public class JachtiesController : Controller
    {
        private WypozyczalniaJachtowEntities1 db = new WypozyczalniaJachtowEntities1();

        // GET: Jachties
        public ActionResult Index()
        {
            return View(db.Jachty.ToList());
        }

        // GET: Jachties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jachty jachty = db.Jachty.Find(id);
            if (jachty == null)
            {
                return HttpNotFound();
            }
            return View(jachty);
        }

        // GET: Jachties/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jachties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Zdjecie,Port,IloscMiejsc,Dlugosc,Szerokosc,Silnik,koszt,Wyposazenie")] Jachty jachty)
        {
            if (ModelState.IsValid)
            {
                db.Jachty.Add(jachty);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jachty);
        }

        // GET: Jachties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jachty jachty = db.Jachty.Find(id);
            if (jachty == null)
            {
                return HttpNotFound();
            }
            return View(jachty);
        }

        // POST: Jachties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Zdjecie,Port,IloscMiejsc,Dlugosc,Szerokosc,Silnik,koszt,Wyposazenie")] Jachty jachty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jachty).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jachty);
        }

        // GET: Jachties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jachty jachty = db.Jachty.Find(id);
            if (jachty == null)
            {
                return HttpNotFound();
            }
            return View(jachty);
        }

        // POST: Jachties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Jachty jachty = db.Jachty.Find(id);
            db.Jachty.Remove(jachty);
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
