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
    public class PhotosController : Controller
    {
        private Entities db = new Entities();

        // GET: Photos
        public ActionResult Index()
        {
            var photos = db.Photos.Include(p => p.AspNetUser).Include(p => p.Discussion);
            return View(photos.ToList());
        }
        // GET: Photos/MyPhotos
        public ActionResult MyPhotos()
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
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _AddPhotoTraveling(int idPhoto,bool main,int idTraveling)
        {
            if (ModelState.IsValid)
            {
                foreach (var i in db.AspNetUsers.ToList())
                {
                    if (i.UserName == User.Identity.Name)
                    {
                        foreach (var j in db.Photos)
                        {
                            if (main==true&&
                                db.Photos.Find(j.Id).Traveling != null &&
                                db.Photos.Find(j.Id).Main==true)
                            {
                                db.Photos.Find(j.Id).Main = false;
                            }
                        }
                        Photo photo = db.Photos.Find(idPhoto);
                        db.Travelings.Find(idTraveling).Photos.Add(photo);
                        db.SaveChanges();
                        return Redirect(Request.UrlReferrer.AbsolutePath);
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyPhotosAdd([Bind(Include = "Id,Path,Description,IdUserCreate,IdDiscussion,Main,DataTimeCreate")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                foreach (var i in db.AspNetUsers.ToList())
                {
                    if (i.UserName == User.Identity.Name)
                    {
                        i.Photos1.Add(db.Photos.Find(photo.Id));
                        db.SaveChanges();
                        return RedirectToAction("MyPhotos");
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(photo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyPhotosDelete([Bind(Include = "Id,Path,Description,IdUserCreate,IdDiscussion,Main,DataTimeCreate")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                foreach (var i in db.AspNetUsers.ToList())
                {
                    if (i.UserName == User.Identity.Name)
                    {
                        if (db.Photos.Find(photo.Id).Main == true)
                        {
                            db.Photos.Find(photo.Id).Main = false;
                        }
                        i.Photos1.Remove(db.Photos.Find(photo.Id));
                        db.SaveChanges();
                        return RedirectToAction("MyPhotos");
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(photo);
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

        // GET: Photos/Create
        public ActionResult Create()
        {
            ViewBag.IdUserCreate = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.IdDiscussion = new SelectList(db.Discussions, "Id", "IdUserCreate");
            return View();
        }

        // POST: Photos/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Path,Description,Main")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                foreach (var i in db.AspNetUsers.ToList())
                {
                    if (i.UserName == User.Identity.Name)
                    {
                        var discussion = new Discussion();
                        discussion.AspNetUser = i;
                        discussion.Title = "Для фото";
                        discussion.DateTimeCreate = DateTimeOffset.Now.DateTime;

                        photo.AspNetUser = i;
                        photo.DataTimeCreate = DateTimeOffset.Now.DateTime;
                        photo.Discussion = discussion;

                        if (photo.Main == true)
                        {
                           var photos= db.Photos.Where(p => p.Main == true && p.AspNetUser.Id == i.Id);
                           foreach (var p in photos.ToList())
                           {
                               if (db.Photos.Find(p.Id).Traveling == null&&
                                   db.Photos.Find(p.Id).Place==null&&
                                   db.Photos.Find(p.Id).Group == null)
                               {
                                   db.Photos.Find(p.Id).Main = false;
                                   db.AspNetUsers.Find(i.Id).Photos1.Add(photo);
                               }
                           }
                        }
                        else
                        {
                            db.AspNetUsers.Find(i.Id).Photos.Add(photo);
                        }
                        db.Photos.Add(photo);
                        db.Discussions.Add(discussion);
                        
                        db.SaveChanges();
                        return RedirectToAction("MyPhotos");
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    
            }
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
            ViewBag.IdGroup = new SelectList(db.Groups, photo.Group);
            ViewBag.IdPlace = new SelectList(db.Places, photo.Place);
            ViewBag.IdTraveling = new SelectList(db.Travelings, photo.Traveling);
            ViewBag.IdUserCreate = new SelectList(db.AspNetUsers, "Id", "Email", photo.IdUserCreate);
            ViewBag.IdDiscussion = new SelectList(db.Discussions, "Id", "IdUserCreate", photo.IdDiscussion);
            return View(photo);
        }

        // POST: Photos/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Path,Description,Main,IdTraveling,IdPlace,IdGroup")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                db.Photos.Find(photo.Id).Path = photo.Path;
                db.Photos.Find(photo.Id).Description = photo.Description;
                db.Photos.Find(photo.Id).Main = photo.Main;
                foreach (var i in db.AspNetUsers.ToList())
                {
                    if (i.UserName == User.Identity.Name)
                    {
                        if (photo.Main == true)
                        {
                            var photos = db.Photos.Where(p => p.Main == true && p.AspNetUser.Id == i.Id);
                            foreach (var p in photos.ToList())
                            {
                                if (db.Photos.Find(photo.Id).Traveling == null &&
                                   db.Photos.Find(photo.Id).Place == null &&
                                   db.Photos.Find(photo.Id).Group == null&&
                                    p.Traveling == null &&
                                   p.Place == null &&
                                   p.Group == null)
                                {
                                    db.Photos.Find(p.Id).Main = false;
                                }
                                else if (db.Photos.Find(photo.Id).Traveling != null&&
                                    p.Traveling != null)
                                {
                                    db.Photos.Find(p.Id).Main = false;
                                }
                                else if (db.Photos.Find(photo.Id).Group != null &&
                                   p.Group != null)
                                {
                                    db.Photos.Find(p.Id).Main = false;
                                }else if (db.Photos.Find(photo.Id).Place != null&&
                                    p.Place != null)
                                {
                                    db.Photos.Find(p.Id).Main = false;
                                }
                            }
                        }
                    }
                }
                db.Entry(db.Photos.Find(photo.Id)).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUserCreate = new SelectList(db.AspNetUsers, "Id", "Email", photo.IdUserCreate);
            ViewBag.IdDiscussion = new SelectList(db.Discussions, "Id", "IdUserCreate", photo.IdDiscussion);
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
            if (photo.Discussion != null)
            {
                db.Discussions.Remove(photo.Discussion);
            }
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
