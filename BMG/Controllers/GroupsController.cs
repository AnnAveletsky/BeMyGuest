using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult> Index()
        {
            var groups = db.Groups.Include(g => g.AspNetUser).Include(g => g.Photo).Include(g => g.Place);
            return View(await groups.ToListAsync());
        }

        // GET: Groups/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = await db.Groups.FindAsync(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            ViewBag.IdUserCreate = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.IdPhoto = new SelectList(db.Photos, "Id", "Path");
            ViewBag.IdPlace = new SelectList(db.Places, "Id", "IdUser");
            return View();
        }

        // POST: Groups/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,IdUserCreate,Description,IdPhoto,IdPlace")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Groups.Add(group);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdUserCreate = new SelectList(db.AspNetUsers, "Id", "Email", group.IdUserCreate);
            ViewBag.IdPhoto = new SelectList(db.Photos, "Id", "Path", group.IdPhoto);
            ViewBag.IdPlace = new SelectList(db.Places, "Id", "IdUser", group.IdPlace);
            return View(group);
        }

        // GET: Groups/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = await db.Groups.FindAsync(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUserCreate = new SelectList(db.AspNetUsers, "Id", "Email", group.IdUserCreate);
            ViewBag.IdPhoto = new SelectList(db.Photos, "Id", "Path", group.IdPhoto);
            ViewBag.IdPlace = new SelectList(db.Places, "Id", "IdUser", group.IdPlace);
            return View(group);
        }

        // POST: Groups/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,IdUserCreate,Description,IdPhoto,IdPlace")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(group).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdUserCreate = new SelectList(db.AspNetUsers, "Id", "Email", group.IdUserCreate);
            ViewBag.IdPhoto = new SelectList(db.Photos, "Id", "Path", group.IdPhoto);
            ViewBag.IdPlace = new SelectList(db.Places, "Id", "IdUser", group.IdPlace);
            return View(group);
        }

        // GET: Groups/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = await db.Groups.FindAsync(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Group group = await db.Groups.FindAsync(id);
            db.Groups.Remove(group);
            await db.SaveChangesAsync();
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
