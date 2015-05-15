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
    public class PhotosController : Controller
    {
        private Entities db = new Entities();

        // GET: Photos
        public ActionResult Index()
        {
            var photos = db.Photos.Include(p => p.AspNetUser).Include(p => p.City).Include(p => p.Country).Include(p => p.Discussion).Include(p => p.Place);
            return View(photos.ToList());
        }

        // GET: Photos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }
        public ActionResult MyPhotoAlbum()
        {

            foreach (BMG.Models.AspNetUser user in db.AspNetUsers.ToList())
            {
                if (user.UserName == User.Identity.Name)
                {
                    return View(user.Photos);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        // POST: Photos/AddToPhotoAlbum/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToPhotoAlbum([Bind(Include = "Id,Path,Description,IdUserCreate")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                foreach (var i in db.AspNetUsers.ToList())
                {
                    if (i.UserName == User.Identity.Name)
                    {
                        photo.ContainsPhotoAlbum = true;
                        db.Entry(photo).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("MyPhotoAlbum");
                    }
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        // GET: Photos/Create
        public ActionResult Create()
        {
            ViewBag.IdUserCreate = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.IdCity = new SelectList(db.Cities, "Id", "Name");
            ViewBag.IdCountry = new SelectList(db.Countries, "Id", "Name");
            ViewBag.IdDiscussion = new SelectList(db.Discussions, "Id", "IdUserCreate");
            ViewBag.IdPlace = new SelectList(db.Places, "Id", "IdUser");
            return View();
        }

        // POST: Photos/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Path,Description,ContainsPhotoAlbum,IdPlace,IdCountry,IdCity")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                foreach (var i in db.AspNetUsers.ToList())
                {
                    if (i.UserName == User.Identity.Name)
                    {
                        photo.IdUserCreate = i.Id;
                        db.Photos.Add(photo);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }

            ViewBag.IdUserCreate = new SelectList(db.AspNetUsers, "Id", "Email", photo.IdUserCreate);
            ViewBag.IdCity = new SelectList(db.Cities, "Id", "Name", photo.IdCity);
            ViewBag.IdCountry = new SelectList(db.Countries, "Id", "Name", photo.IdCountry);
            ViewBag.IdDiscussion = new SelectList(db.Discussions, "Id", "IdUserCreate", photo.IdDiscussion);
            ViewBag.IdPlace = new SelectList(db.Places, "Id", "IdUser", photo.IdPlace);
            return View(photo);
        }

        // GET: Photos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUserCreate = new SelectList(db.AspNetUsers, "Id", "Email", photo.IdUserCreate);
            ViewBag.IdCity = new SelectList(db.Cities, "Id", "Name", photo.IdCity);
            ViewBag.IdCountry = new SelectList(db.Countries, "Id", "Name", photo.IdCountry);
            ViewBag.IdDiscussion = new SelectList(db.Discussions, "Id", "IdUserCreate", photo.IdDiscussion);
            ViewBag.IdPlace = new SelectList(db.Places, "Id", "IdUser", photo.IdPlace);
            return View(photo);
        }

        // POST: Photos/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Path,Description,IdUserCreate,ContainsPhotoAlbum,IdPlace,IdDiscussion,IdCountry,IdCity")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(photo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUserCreate = new SelectList(db.AspNetUsers, "Id", "Email", photo.IdUserCreate);
            ViewBag.IdCity = new SelectList(db.Cities, "Id", "Name", photo.IdCity);
            ViewBag.IdCountry = new SelectList(db.Countries, "Id", "Name", photo.IdCountry);
            ViewBag.IdDiscussion = new SelectList(db.Discussions, "Id", "IdUserCreate", photo.IdDiscussion);
            ViewBag.IdPlace = new SelectList(db.Places, "Id", "IdUser", photo.IdPlace);
            return View(photo);
        }

        // GET: Photos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: Photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Photo photo = db.Photos.Find(id);
            db.Photos.Remove(photo);
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
