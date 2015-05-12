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
    public class UsersController : Controller
    {
        private Entities db = new Entities();

        // GET: Users
        public ActionResult Index()
        {
            var aspNetUsers = db.AspNetUsers.Include(a => a.City).Include(a => a.Country).Include(a => a.Photo);
            return View(aspNetUsers.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }
        // GET: Users/Details/5
        public ActionResult AboutMe()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string id = null;
            for (int i = 0; i < db.AspNetUsers.ToList().Count; i++)
            {
                if (db.AspNetUsers.ToList()[i].UserName == User.Identity.Name)
                {
                    id = db.AspNetUsers.ToList()[i].Id;
                }
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null || db.AspNetUsers.Find(id).UserName!=User.Identity.Name)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCity = new SelectList(db.Cities, "Id", "Name", aspNetUser.IdCity);
            ViewBag.IdCountry = new SelectList(db.Countries, "Id", "Name", aspNetUser.IdCountry);
            ViewBag.IdPhoto = new SelectList(db.Photos, "Id", "Path", aspNetUser.IdPhoto);
            return View(aspNetUser);
        }

        // POST: Users/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,SecondName,Status,Email,PhoneNumber,IdPhoto,DataBirthday,IdCountry,IdCity,InfoAboutMe")] AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                db.AspNetUsers.Find(aspNetUser.Id).FirstName = aspNetUser.FirstName;
                db.AspNetUsers.Find(aspNetUser.Id).SecondName = aspNetUser.SecondName;
                db.AspNetUsers.Find(aspNetUser.Id).Status = aspNetUser.Status;
                db.AspNetUsers.Find(aspNetUser.Id).Email = aspNetUser.Email;
                db.AspNetUsers.Find(aspNetUser.Id).PhoneNumber = aspNetUser.PhoneNumber;
                db.AspNetUsers.Find(aspNetUser.Id).IdPhoto = aspNetUser.IdPhoto;
                db.AspNetUsers.Find(aspNetUser.Id).DataBirthday = aspNetUser.DataBirthday;
                db.AspNetUsers.Find(aspNetUser.Id).IdCountry = aspNetUser.IdCountry;
                db.AspNetUsers.Find(aspNetUser.Id).IdCity = aspNetUser.IdCity;
                db.SaveChanges();
                return RedirectToAction("AboutMe");
            }
            
            ViewBag.IdCountry = new SelectList(db.Countries, "Id", "Name", aspNetUser.IdCountry);
            ViewBag.IdCity = new SelectList(db.Cities, "Id", "Name", aspNetUser.IdCity);
            ViewBag.IdPhoto = new SelectList(db.Photos, "Id", "Path", aspNetUser.IdPhoto);
            return View(aspNetUser);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspNetUser);
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
