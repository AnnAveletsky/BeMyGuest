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
    public class UsersController : Controller
    {
        private Entities db = new Entities();

        // GET: Users
        public ActionResult Index(string status,string firstName,string secondName)
        {
            IEnumerable<AspNetUser> users = db.AspNetUsers.ToList(); ;
            if (status != null)
            {
                users = users.Where(p => p.Status == status);
            }
            if (firstName != null)
            {
                users = users.Where(p => p.FirstName == firstName);
            }
            if (secondName != null)
            {
                users = users.Where(p => p.SecondName == secondName);
            }
            return View(users);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Status,FirstName,SecondName")] AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index",new {aspNetUser.Status,aspNetUser.FirstName,aspNetUser.SecondName});
            }
            return View(aspNetUser);
        }
        // GET: Users/AboutMe
        public ActionResult AboutMe()
        {
            foreach (var i in  db.AspNetUsers.ToList())
            {
                if (i.UserName == User.Identity.Name)
                {
                    return View(i);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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

       

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
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

        // POST: Users/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PhoneNumber,DataBirthday,FirstName,SecondName,InfoAboutMe,Status,Passport")] AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                foreach (var i in db.AspNetUsers.ToList())
                {
                    if (i.UserName == User.Identity.Name)
                    {
                        i.PhoneNumber = aspNetUser.PhoneNumber;
                        i.DataBirthday = aspNetUser.DataBirthday;
                        i.FirstName = aspNetUser.FirstName;
                        i.SecondName = aspNetUser.SecondName;
                        i.InfoAboutMe = aspNetUser.InfoAboutMe;
                        i.Status = aspNetUser.Status;
                        i.Passport = aspNetUser.Passport;
                        db.Entry(i).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("AboutMe");
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
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
