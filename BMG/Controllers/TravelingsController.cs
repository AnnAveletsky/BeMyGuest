﻿using System;
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
    public class TravelingsController : Controller
    {
        private Entities db = new Entities();

        // GET: Travelings
        public ActionResult Index()
        {
            var travelings = db.Travelings.Include(t => t.AspNetUser).Include(t => t.City).Include(t => t.Country).Include(t => t.Discussion).Include(t => t.Photo);
            return View(travelings.ToList());
        }

        // GET: Travelings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Traveling traveling = db.Travelings.Find(id);
            if (traveling == null)
            {
                return HttpNotFound();
            }
            return View(traveling);
        }

        // GET: Travelings/Create
        public ActionResult Create()
        {
            ViewBag.IdUserCreate = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.IdCity = new SelectList(db.Cities, "Id", "Name");
            ViewBag.IdCountry = new SelectList(db.Countries, "Id", "Name");
            ViewBag.IdDiscussion = new SelectList(db.Discussions, "Id", "IdUserCreate");
            ViewBag.IdPhoto = new SelectList(db.Photos, "Id", "Path");
            return View();
        }

        // POST: Travelings/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,DateTimeComing,DateTimeDeparture,Description,IdCountry,IdCity,IdUserCreate,IdPhoto,IdDiscussion")] Traveling traveling)
        {
            if (ModelState.IsValid)
            {
                db.Travelings.Add(traveling);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdUserCreate = new SelectList(db.AspNetUsers, "Id", "Email", traveling.IdUserCreate);
            ViewBag.IdCity = new SelectList(db.Cities, "Id", "Name", traveling.IdCity);
            ViewBag.IdCountry = new SelectList(db.Countries, "Id", "Name", traveling.IdCountry);
            ViewBag.IdDiscussion = new SelectList(db.Discussions, "Id", "IdUserCreate", traveling.IdDiscussion);
            ViewBag.IdPhoto = new SelectList(db.Photos, "Id", "Path", traveling.IdPhoto);
            return View(traveling);
        }

        // GET: Travelings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Traveling traveling = db.Travelings.Find(id);
            if (traveling == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUserCreate = new SelectList(db.AspNetUsers, "Id", "Email", traveling.IdUserCreate);
            ViewBag.IdCity = new SelectList(db.Cities, "Id", "Name", traveling.IdCity);
            ViewBag.IdCountry = new SelectList(db.Countries, "Id", "Name", traveling.IdCountry);
            ViewBag.IdDiscussion = new SelectList(db.Discussions, "Id", "IdUserCreate", traveling.IdDiscussion);
            ViewBag.IdPhoto = new SelectList(db.Photos, "Id", "Path", traveling.IdPhoto);
            return View(traveling);
        }

        // POST: Travelings/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,DateTimeComing,DateTimeDeparture,Description,IdCountry,IdCity,IdUserCreate,IdPhoto,IdDiscussion")] Traveling traveling)
        {
            if (ModelState.IsValid)
            {
                db.Entry(traveling).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUserCreate = new SelectList(db.AspNetUsers, "Id", "Email", traveling.IdUserCreate);
            ViewBag.IdCity = new SelectList(db.Cities, "Id", "Name", traveling.IdCity);
            ViewBag.IdCountry = new SelectList(db.Countries, "Id", "Name", traveling.IdCountry);
            ViewBag.IdDiscussion = new SelectList(db.Discussions, "Id", "IdUserCreate", traveling.IdDiscussion);
            ViewBag.IdPhoto = new SelectList(db.Photos, "Id", "Path", traveling.IdPhoto);
            return View(traveling);
        }

        // GET: Travelings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Traveling traveling = db.Travelings.Find(id);
            if (traveling == null)
            {
                return HttpNotFound();
            }
            return View(traveling);
        }

        // POST: Travelings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Traveling traveling = db.Travelings.Find(id);
            db.Travelings.Remove(traveling);
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
