using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BMG.Models;

namespace BMG.Controllers
{
    public class OccasionsController : Controller
    {
        private Entities db = new Entities();

        // GET: Occasions
        public ActionResult Index()
        {
            var occasions = db.Occasions.Include(o => o.AspNetUser).Include(o => o.AspNetUser1).Include(o => o.Place).Include(o => o.Traveling);
            return View(occasions.ToList());
        }
        // GET: Occasions/MyOccasions
        public ActionResult MyOccasions()
        {
            foreach (var i in db.AspNetUsers.ToList())
            {
                if (i.UserName == User.Identity.Name)
                {
                    return View(i);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        // GET: Occasions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Occasion occasion = db.Occasions.Find(id);
            if (occasion == null)
            {
                return HttpNotFound();
            }
            return View(occasion);
        }

        // GET: Occasions/Create
        public ActionResult Create()
        {
            ViewBag.IdUserHost = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.IdUserTraveler = new SelectList(db.AspNetUsers, "Id", "Email");
        
            return View();
        }

        // POST: Occasions/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdUserHost,Description,ArrivalDate,CheckOut")] Occasion occasion)
        {
            if (ModelState.IsValid)
            {
                foreach (var i in db.AspNetUsers.ToList())
                {
                    if (i.UserName == User.Identity.Name)
                    {

                        occasion.AspNetUser1 = db.AspNetUsers.Find(i.Id);
                        occasion.DataTimeCreate = DateTimeOffset.Now.DateTime;
                        occasion.Status = "Не состоялось";
                        occasion.CommentHost = "";
                        occasion.CommentTreveler="";

                        db.Occasions.Add(occasion);
                        db.SaveChanges();
                        return RedirectToAction("MyOccasions");
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                
            }

            ViewBag.IdUserHost = new SelectList(db.AspNetUsers, "Id", "Email", occasion.IdUserHost);
            ViewBag.IdUserTraveler = new SelectList(db.AspNetUsers, "Id", "Email", occasion.IdUserTraveler);
            ViewBag.IdPlace = new SelectList(db.Places, "Id", "IdUser", occasion.IdPlace);
            ViewBag.IdTraveling = new SelectList(db.Travelings, "Id", "Name", occasion.IdTraveling);
            return View(occasion);
        }

        // GET: Occasions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Occasion occasion = db.Occasions.Find(id);
            if (occasion == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUserHost = new SelectList(db.AspNetUsers, "Id", "Email", occasion.IdUserHost);
            ViewBag.IdUserTraveler = new SelectList(db.AspNetUsers, "Id", "Email", occasion.IdUserTraveler);
            ViewBag.IdPlace = new SelectList(db.Places, "Id", "IdUser", occasion.IdPlace);
            ViewBag.IdTraveling = new SelectList(db.Travelings, "Id", "Name", occasion.IdTraveling);
            return View(occasion);
        }

        // POST: Occasions/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdUserTraveler,IdUserHost,Description,Status,ArrivalDate,CheckOut,CommentTreveler,CommentHost,IdTraveling,IdPlace,DataTimeCreate")] Occasion occasion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(occasion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUserHost = new SelectList(db.AspNetUsers, "Id", "Email", occasion.IdUserHost);
            ViewBag.IdUserTraveler = new SelectList(db.AspNetUsers, "Id", "Email", occasion.IdUserTraveler);
            ViewBag.IdPlace = new SelectList(db.Places, "Id", "IdUser", occasion.IdPlace);
            ViewBag.IdTraveling = new SelectList(db.Travelings, "Id", "Name", occasion.IdTraveling);
            return View(occasion);
        }

        // GET: Occasions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Occasion occasion = db.Occasions.Find(id);
            if (occasion == null)
            {
                return HttpNotFound();
            }
            return View(occasion);
        }

        // POST: Occasions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Occasion occasion = db.Occasions.Find(id);
            db.Occasions.Remove(occasion);
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
