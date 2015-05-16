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
    public class PlacesController : Controller
    {
        private Entities db = new Entities();

        // GET: Places
        public ActionResult Index()
        {
            var places = db.Places.Include(p => p.AspNetUser).Include(p => p.City).Include(p => p.Country).Include(p => p.Discussion).Include(p => p.Group).Include(p => p.Photo);
            return View(places.ToList());
        }
        public ActionResult MyPlaces()
        {
            var places = db.Places.Include(p => p.AspNetUser).Include(p => p.City).Include(p => p.Country).Include(p => p.Discussion).Include(p => p.Group).Include(p => p.Photo);
            return View(places.ToList());
        }
        // GET: Places/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // GET: Places/Create
        public ActionResult Create()
        {
            ViewBag.IdUser = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.IdCity = new SelectList(db.Cities, "Id", "Name");
            ViewBag.IdCountry = new SelectList(db.Countries, "Id", "Name");
            ViewBag.IdDiscussion = new SelectList(db.Discussions, "Id", "IdUserCreate");
            ViewBag.IdGroup = new SelectList(db.Groups, "Id", "Name");
            ViewBag.IdPhoto = new SelectList(db.Photos, "Id", "Path");
            return View();
        }

        // POST: Places/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdUser,Adress,Type,Description,IdCountry,IdCity,Status,IdPhoto,IdDiscussion,IdGroup")] Place place)
        {
            if (ModelState.IsValid)
            {
                db.Places.Add(place);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdUser = new SelectList(db.AspNetUsers, "Id", "Email", place.IdUser);
            ViewBag.IdCity = new SelectList(db.Cities, "Id", "Name", place.IdCity);
            ViewBag.IdCountry = new SelectList(db.Countries, "Id", "Name", place.IdCountry);
            ViewBag.IdDiscussion = new SelectList(db.Discussions, "Id", "IdUserCreate", place.IdDiscussion);
            ViewBag.IdGroup = new SelectList(db.Groups, "Id", "Name", place.IdGroup);
            ViewBag.IdPhoto = new SelectList(db.Photos, "Id", "Path", place.IdPhoto);
            return View(place);
        }

        // GET: Places/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUser = new SelectList(db.AspNetUsers, "Id", "Email", place.IdUser);
            ViewBag.IdCity = new SelectList(db.Cities, "Id", "Name", place.IdCity);
            ViewBag.IdCountry = new SelectList(db.Countries, "Id", "Name", place.IdCountry);
            ViewBag.IdDiscussion = new SelectList(db.Discussions, "Id", "IdUserCreate", place.IdDiscussion);
            ViewBag.IdGroup = new SelectList(db.Groups, "Id", "Name", place.IdGroup);
            ViewBag.IdPhoto = new SelectList(db.Photos, "Id", "Path", place.IdPhoto);
            return View(place);
        }

        // POST: Places/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdUser,Adress,Type,Description,IdCountry,IdCity,Status,IdPhoto,IdDiscussion,IdGroup")] Place place)
        {
            if (ModelState.IsValid)
            {
                db.Entry(place).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUser = new SelectList(db.AspNetUsers, "Id", "Email", place.IdUser);
            ViewBag.IdCity = new SelectList(db.Cities, "Id", "Name", place.IdCity);
            ViewBag.IdCountry = new SelectList(db.Countries, "Id", "Name", place.IdCountry);
            ViewBag.IdDiscussion = new SelectList(db.Discussions, "Id", "IdUserCreate", place.IdDiscussion);
            ViewBag.IdGroup = new SelectList(db.Groups, "Id", "Name", place.IdGroup);
            ViewBag.IdPhoto = new SelectList(db.Photos, "Id", "Path", place.IdPhoto);
            return View(place);
        }

        // GET: Places/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // POST: Places/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Place place = db.Places.Find(id);
            db.Places.Remove(place);
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
