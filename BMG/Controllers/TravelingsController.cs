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
    public class TravelingsController : Controller
    {
        private Entities db = new Entities();

        // GET: Travelings
        public ActionResult Index()
        {
            var travelings = db.Travelings.Include(t => t.AspNetUser);
            return View(travelings.ToList());
        }
        // GET: Travelings/MyTravelings
        public ActionResult MyTravelings()
        {
            foreach (var i in db.AspNetUsers)
            {
                if (i.UserName == User.Identity.Name)
                {
                    return View(i);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
            return View();
        }

        // POST: Travelings/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,DateTimeComing,DateTimeDeparture,Description,IdUserCreate,DataTimeCreate")] Traveling traveling)
        {
            if (ModelState.IsValid)
            {
                foreach (var i in db.AspNetUsers.ToList())
                {
                    if (i.UserName == User.Identity.Name)
                    {
                        var discussion = new Discussion();
                        discussion.AspNetUser = i;
                        discussion.Title = "Для фото" + i.FirstName + " " + i.SecondName;
                        discussion.DateTimeCreate = DateTimeOffset.Now.DateTime;
                        traveling.AspNetUser = db.AspNetUsers.Find(i.Id);
                        traveling.DataTimeCreate = DateTimeOffset.Now.DateTime;
                        traveling.Discussion = discussion;
                        db.Travelings.Add(traveling);
                        db.Discussions.Add(discussion);
                        db.Entry(i).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.IdUserCreate = new SelectList(db.AspNetUsers, "Id", "Email", traveling.IdUserCreate);
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
            return View(traveling);
        }

        // POST: Travelings/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,DateTimeComing,DateTimeDeparture,Description,IdUserCreate,DataTimeCreate")] Traveling traveling)
        {
            if (ModelState.IsValid)
            {
                db.Entry(traveling).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUserCreate = new SelectList(db.AspNetUsers, "Id", "Email", traveling.IdUserCreate);
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
