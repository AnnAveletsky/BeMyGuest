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
    public class GroupsController : Controller
    {
        private Entities db = new Entities();

        // GET: Groups
        public ActionResult Index(string name)
        {
            var groups = db.Groups.Include(g => g.AspNetUser);
            if (name != null && name != "")
            {
                groups = groups.Where(p => p.Name == name);
            }
            return View(groups.ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Id,Name")] Group group)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", new { group.Name });
            }
            return View(group);
        }
        // GET: Groups/MyGroups
        public ActionResult MyGroups()
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
        // GET: Groups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DetailsAdd([Bind(Include = "Id")] Group group)
        {
            if (ModelState.IsValid)
            {
                foreach (var i in db.AspNetUsers.ToList())
                {
                    if (i.UserName == User.Identity.Name)
                    {
                        if (!i.Groups1.Contains(db.Groups.Find(group.Id)))
                        {
                            i.Groups1.Add(db.Groups.Find(group.Id));
                        }
                        db.SaveChanges();
                        return RedirectToAction("Details","Groups",group);
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.IdUserCreate = new SelectList(db.AspNetUsers, "Id", "Email", group.IdUserCreate);
            return View(group);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DetailsDelete([Bind(Include = "Id")] Group group)
        {
            if (ModelState.IsValid)
            {
                foreach (var i in db.AspNetUsers.ToList())
                {
                    if (i.UserName == User.Identity.Name)
                    {
                        if (i.Groups1.Contains(db.Groups.Find(group.Id)))
                        {
                            i.Groups1.Remove(db.Groups.Find(group.Id));
                        }
                        db.SaveChanges();
                        return RedirectToAction("Details", "Groups", group);
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.IdUserCreate = new SelectList(db.AspNetUsers, "Id", "Email", group.IdUserCreate);
            return View(group);
        }
        // GET: Groups/Create
        public ActionResult Create()
        {
            ViewBag.IdUserCreate = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Groups/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] Group group)
        {
            if (ModelState.IsValid)
            {
                foreach (var i in db.AspNetUsers.ToList())
                {
                    if (i.UserName == User.Identity.Name)
                    {
                        group.AspNetUser = db.AspNetUsers.Find(i.Id);
                        group.DataTimeCreate = DateTimeOffset.Now.DateTime;
                        db.Groups.Add(group);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.IdUserCreate = new SelectList(db.AspNetUsers, "Id", "Email", group.IdUserCreate);
            return View(group);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUserCreate = new SelectList(db.AspNetUsers, "Id", "Email", group.IdUserCreate);
            return View(group);
        }

        // POST: Groups/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Groups.Find(group.Id).Name = group.Name;
                db.Groups.Find(group.Id).Description = group.Description;
                db.Entry(db.Groups.Find(group.Id)).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUserCreate = new SelectList(db.AspNetUsers, "Id", "Email", group.IdUserCreate);
            return View(group);
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Group group = db.Groups.Find(id);
            db.Groups.Remove(group);
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
