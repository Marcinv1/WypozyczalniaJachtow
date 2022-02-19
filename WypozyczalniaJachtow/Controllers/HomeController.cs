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
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Inland()
        {
            ViewBag.Message = "Your inland navigation.";

            WypozyczalniaJachtowEntities1 db = new WypozyczalniaJachtowEntities1();

            List<Jachty> jachtylista = db.Jachty.ToList();

            JachtyView jachtyVM = new JachtyView();

            List<JachtyView> jachtyVMlista = jachtylista.Select(x => new JachtyView
            { ID = x.ID, Name = x.Name,
                Zdjecie = x.Zdjecie,
                Port = x.Port,
                IloscMiejsc = x.IloscMiejsc,
                Dlugosc = x.Dlugosc,
                Szerokosc = x.Szerokosc,
                Silnik = x.Silnik,
                Koszt = x.koszt,
                Wyposazenie = x.Wyposazenie,
                TypJachtu = x.TypJachtu}).ToList();
            return View(jachtyVMlista);
        }
        public ActionResult Rental11111()
        {
            ViewBag.Message = "Your rental screen.";
            return View();
        }
        public ActionResult SaveRecord(RezerwacjeView model)
        {

            try
            {
                WypozyczalniaJachtowEntities1 db = new WypozyczalniaJachtowEntities1();

                Rezerwacja emp = new Rezerwacja
                {
                    JachtID = model.JachtID,
                    Imie = model.Imie,
                    NrTel = model.NrTel,
                    DataDo = model.Od,
                    DataOd = model.DO,
                    Zaliczka = model.Zaliczka
                };

                db.Rezerwacja.Add(emp);
                db.SaveChanges();

                int latestEmpId = emp.ID;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Rental111");
        }
        public ActionResult Sea()
        {
            ViewBag.Message = "Your sea screen";

            WypozyczalniaJachtowEntities1 db = new WypozyczalniaJachtowEntities1();

            List<Jachty> jachtylista = db.Jachty.ToList();

            JachtyView jachtyVM = new JachtyView();

            List<JachtyView> jachtyVMlista = jachtylista.Select(x => new JachtyView
            {
                ID = x.ID,
                Name = x.Name,
                Zdjecie = x.Zdjecie,
                Port = x.Port,
                IloscMiejsc = x.IloscMiejsc,
                Dlugosc = x.Dlugosc,
                Szerokosc = x.Szerokosc,
                Silnik = x.Silnik,
                Koszt = x.koszt,
                Wyposazenie = x.Wyposazenie,
                TypJachtu = x.TypJachtu
                
            }).ToList();

            return View(jachtyVMlista);
        }









        public class RezerwacjasController : Controller
        {
            private WypozyczalniaJachtowEntities1 db = new WypozyczalniaJachtowEntities1();

            // GET: Rezerwacjas
            public ActionResult Rental()
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
                ViewBag.JachtID = new SelectList(db.Jachty, "ID", "Name");
                return View();
            }

            // POST: Rezerwacjas/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create([Bind(Include = "ID,JachtID,Imie,NrTel,DataOd,DataDo,Zaliczka")] Rezerwacja rezerwacja)
            {
                if (ModelState.IsValid)
                {
                    db.Rezerwacja.Add(rezerwacja);
                    db.SaveChanges();
                    return RedirectToAction("Rental");
                }

                ViewBag.JachtID = new SelectList(db.Jachty, "ID", "Name", rezerwacja.JachtID);
                return View(rezerwacja);
            }

            // GET: Rezerwacjas/Edit/5
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

            // POST: Rezerwacjas/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit([Bind(Include = "ID,JachtID,Imie,NrTel,DataOd,DataDo,Zaliczka")] Rezerwacja rezerwacja)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(rezerwacja).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Rental");
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
                return RedirectToAction("Rental");
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


    
}