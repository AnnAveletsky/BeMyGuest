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
    public class DiscussionsController : Controller
    {
        private Entities db = new Entities();

        // GET: Discussions
        public async Task<ActionResult> Index()
        {
            var discussions = db.Discussions.Include(d => d.AspNetUser).Include(d => d.Group).Include(d => d.Place);
            return View(await discussions.ToListAsync());
        }

        // GET: Discussions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discussion discussion = await db.Discussions.FindAsync(id);
            if (discussion == null)
            {
                return HttpNotFound();
            }
            return View(discussion);
        }

        // GET: Discussions/Create
        public ActionResult Create()
        {
            ViewBag.IdUserCreate = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.IdGroup = new SelectList(db.Groups, "Id", "Name");
            ViewBag.IdPlace = new SelectList(db.Places, "Id", "IdUser");
            return View();
        }

        // POST: Discussions/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,IdUserCreate,Type,Title,DateTimeCreate,IdGroup,IdPlace")] Discussion discussion)
        {
            if (ModelState.IsValid)
            {
                db.Discussions.Add(discussion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdUserCreate = new SelectList(db.AspNetUsers, "Id", "Email", discussion.IdUserCreate);
            ViewBag.IdGroup = new SelectList(db.Groups, "Id", "Name", discussion.IdGroup);
            ViewBag.IdPlace = new SelectList(db.Places, "Id", "IdUser", discussion.IdPlace);
            return View(discussion);
        }

        // GET: Discussions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discussion discussion = await db.Discussions.FindAsync(id);
            if (discussion == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUserCreate = new SelectList(db.AspNetUsers, "Id", "Email", discussion.IdUserCreate);
            ViewBag.IdGroup = new SelectList(db.Groups, "Id", "Name", discussion.IdGroup);
            ViewBag.IdPlace = new SelectList(db.Places, "Id", "IdUser", discussion.IdPlace);
            return View(discussion);
        }

        // POST: Discussions/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,IdUserCreate,Type,Title,DateTimeCreate,IdGroup,IdPlace")] Discussion discussion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(discussion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdUserCreate = new SelectList(db.AspNetUsers, "Id", "Email", discussion.IdUserCreate);
            ViewBag.IdGroup = new SelectList(db.Groups, "Id", "Name", discussion.IdGroup);
            ViewBag.IdPlace = new SelectList(db.Places, "Id", "IdUser", discussion.IdPlace);
            return View(discussion);
        }

        // GET: Discussions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discussion discussion = await db.Discussions.FindAsync(id);
            if (discussion == null)
            {
                return HttpNotFound();
            }
            return View(discussion);
        }

        // POST: Discussions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Discussion discussion = await db.Discussions.FindAsync(id);
            db.Discussions.Remove(discussion);
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
