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
    public class PhotosController : Controller
    {
        private Entities db = new Entities();

        // GET: Photos
        public async Task<ActionResult> Index()
        {
            var photos = db.Photos.Include(p => p.AspNetUser).Include(p => p.Discussion).Include(p => p.Place);
            return View(await photos.ToListAsync());
        }

        // GET: Photos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = await db.Photos.FindAsync(id);
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
            ViewBag.IdPlace = new SelectList(db.Places, "Id", "IdUser");
            return View();
        }

        // POST: Photos/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Path,Description,IdUserCreate,ContainsPhotoAlbum,IdPlace,IdDiscussion")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                db.Photos.Add(photo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdUserCreate = new SelectList(db.AspNetUsers, "Id", "Email", photo.IdUserCreate);
            ViewBag.IdDiscussion = new SelectList(db.Discussions, "Id", "IdUserCreate", photo.IdDiscussion);
            ViewBag.IdPlace = new SelectList(db.Places, "Id", "IdUser", photo.IdPlace);
            return View(photo);
        }

        // GET: Photos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = await db.Photos.FindAsync(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUserCreate = new SelectList(db.AspNetUsers, "Id", "Email", photo.IdUserCreate);
            ViewBag.IdDiscussion = new SelectList(db.Discussions, "Id", "IdUserCreate", photo.IdDiscussion);
            ViewBag.IdPlace = new SelectList(db.Places, "Id", "IdUser", photo.IdPlace);
            return View(photo);
        }

        // POST: Photos/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Path,Description,IdUserCreate,ContainsPhotoAlbum,IdPlace,IdDiscussion")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(photo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdUserCreate = new SelectList(db.AspNetUsers, "Id", "Email", photo.IdUserCreate);
            ViewBag.IdDiscussion = new SelectList(db.Discussions, "Id", "IdUserCreate", photo.IdDiscussion);
            ViewBag.IdPlace = new SelectList(db.Places, "Id", "IdUser", photo.IdPlace);
            return View(photo);
        }

        // GET: Photos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = await db.Photos.FindAsync(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: Photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Photo photo = await db.Photos.FindAsync(id);
            db.Photos.Remove(photo);
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
