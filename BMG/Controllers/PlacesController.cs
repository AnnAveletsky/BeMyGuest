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
        public ActionResult Index(string adress, string type, int? idCountry, int? IdCity,string status,bool? main)
        {
            var places = db.Places.Include(p => p.AspNetUser).Include(p => p.City).Include(p => p.Country).Include(p => p.Discussion);
            if (idCountry != null&&db.Countries.Find(idCountry).Name!="")
            {
                places = places.Where(p => p.Country.Id==idCountry);
            }
            if (IdCity != null && db.Cities.Find(IdCity).Name != "")
            {
                places = places.Where(p => p.City.Id == IdCity);
            }
            if(adress!=null){
                places = places.Where(p => p.Adress.Contains(adress));
            }
            if (type != null)
            {
                places = places.Where(p => p.Type.Contains(type));
            }
            if (status != null)
            {
                places = places.Where(p => p.Status.Contains(status));
            }
            if (main != null)
            {
                places = places.Where(p => p.Main==main);
            }
            ViewBag.IdUser = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.IdCity = new SelectList(db.Cities, "Id", "Name");
            ViewBag.IdCountry = new SelectList(db.Countries, "Id", "Name");
            ViewBag.IdDiscussion = new SelectList(db.Discussions, "Id", "IdUserCreate");
            return View(places);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Id,Adress,Type,IdCountry,IdCity,Status,Main")] Place place)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", new { place.Adress, place.Type, place.IdCountry, place.IdCity, place.Status, place.Main });
            }
            return View(place);
        }
        // GET: Places/MyPlaces
        public ActionResult MyPlaces()
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
            return View();
        }

        // POST: Places/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdUser,Adress,Type,Description,IdCountry,IdCity,Status,Main,IdDiscussion")] Place place)
        {
            if (ModelState.IsValid)
            {
                foreach (var i in db.AspNetUsers.ToList())
                {
                    if (i.UserName == User.Identity.Name)
                    {
                        var discussion = new Discussion();
                        discussion.AspNetUser = i;
                        discussion.Title = "Для места" + i.FirstName + " " + i.SecondName;
                        discussion.DateTimeCreate = DateTimeOffset.Now.DateTime;

                        place.AspNetUser = i;
                        place.Discussion = discussion;
                        discussion.Places.Add(place);

                        if (place.Main == true)
                        {
                            var photos = db.Photos.Where(p => p.Main == true && p.AspNetUser.Id == i.Id);
                            foreach (var p in photos.ToList())
                            {
                                db.Photos.Find(p.Id).Main = false;
                            }
                        }
                        db.Places.Add(place);
                        db.Discussions.Add(discussion);
                        db.AspNetUsers.Find(i.Id).Places.Add(place);
                        db.SaveChanges();
                        return RedirectToAction("MyPlaces");
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.IdUser = new SelectList(db.AspNetUsers, "Id", "Email", place.IdUser);
            ViewBag.IdCity = new SelectList(db.Cities, "Id", "Name", place.IdCity);
            ViewBag.IdCountry = new SelectList(db.Countries, "Id", "Name", place.IdCountry);
            ViewBag.IdDiscussion = new SelectList(db.Discussions, "Id", "IdUserCreate", place.IdDiscussion);
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
            return View(place);
        }

        // POST: Places/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Adress,Type,Description,IdCountry,IdCity,Status,Main")] Place place)
        {
            if (ModelState.IsValid)
            {
                db.Places.Find(place.Id).Adress=place.Adress;
                db.Places.Find(place.Id).Type = place.Type;
                db.Places.Find(place.Id).Description = place.Description;
                db.Places.Find(place.Id).IdCountry = place.IdCountry;
                db.Places.Find(place.Id).IdCity = place.IdCity;
                db.Places.Find(place.Id).Status = place.Status;
                db.Places.Find(place.Id).Status = place.Status;
                db.Places.Find(place.Id).Main = place.Main;
                db.Entry(db.Places.Find(place.Id)).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUser = new SelectList(db.AspNetUsers, "Id", "Email", place.IdUser);
            ViewBag.IdCity = new SelectList(db.Cities, "Id", "Name", place.IdCity);
            ViewBag.IdCountry = new SelectList(db.Countries, "Id", "Name", place.IdCountry);
            ViewBag.IdDiscussion = new SelectList(db.Discussions, "Id", "IdUserCreate", place.IdDiscussion);
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
