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
    public class OccasionsController : Controller
    {
        private Entities db = new Entities();

        // GET: Occasions
        public async Task<ActionResult> Index()
        {
            var occasions = db.Occasions.Include(o => o.AspNetUser).Include(o => o.AspNetUser1).Include(o => o.Place).Include(o => o.Traveling);
            return View(await occasions.ToListAsync());
        }

        // GET: Occasions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Occasion occasion = await db.Occasions.FindAsync(id);
            if (occasion == null)
            {
                return HttpNotFound();
            }
            return View(occasion);
        }

        // GET: Occasions/Create
        public ActionResult Create()
        {
            ViewBag.IdUserHost = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.IdUserTraveler = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.IdPlace = new SelectList(db.Places, "Id", "IdUser");
            ViewBag.IdTraveling = new SelectList(db.Travelings, "Id", "Name");
            return View();
        }

        // POST: Occasions/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,IdUserTraveler,IdUserHost,Description,Status,ArrivalDate,CheckOut,CommentTreveler,CommentHost,IdTraveling,IdPlace,CommentPlaceHost")] Occasion occasion)
        {
            if (ModelState.IsValid)
            {
                db.Occasions.Add(occasion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdUserHost = new SelectList(db.AspNetUsers, "Id", "Email", occasion.IdUserHost);
            ViewBag.IdUserTraveler = new SelectList(db.AspNetUsers, "Id", "Email", occasion.IdUserTraveler);
            ViewBag.IdPlace = new SelectList(db.Places, "Id", "IdUser", occasion.IdPlace);
            ViewBag.IdTraveling = new SelectList(db.Travelings, "Id", "Name", occasion.IdTraveling);
            return View(occasion);
        }

        // GET: Occasions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Occasion occasion = await db.Occasions.FindAsync(id);
            if (occasion == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUserHost = new SelectList(db.AspNetUsers, "Id", "Email", occasion.IdUserHost);
            ViewBag.IdUserTraveler = new SelectList(db.AspNetUsers, "Id", "Email", occasion.IdUserTraveler);
            ViewBag.IdPlace = new SelectList(db.Places, "Id", "IdUser", occasion.IdPlace);
            ViewBag.IdTraveling = new SelectList(db.Travelings, "Id", "Name", occasion.IdTraveling);
            return View(occasion);
        }

        // POST: Occasions/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,IdUserTraveler,IdUserHost,Description,Status,ArrivalDate,CheckOut,CommentTreveler,CommentHost,IdTraveling,IdPlace,CommentPlaceHost")] Occasion occasion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(occasion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdUserHost = new SelectList(db.AspNetUsers, "Id", "Email", occasion.IdUserHost);
            ViewBag.IdUserTraveler = new SelectList(db.AspNetUsers, "Id", "Email", occasion.IdUserTraveler);
            ViewBag.IdPlace = new SelectList(db.Places, "Id", "IdUser", occasion.IdPlace);
            ViewBag.IdTraveling = new SelectList(db.Travelings, "Id", "Name", occasion.IdTraveling);
            return View(occasion);
        }

        // GET: Occasions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Occasion occasion = await db.Occasions.FindAsync(id);
            if (occasion == null)
            {
                return HttpNotFound();
            }
            return View(occasion);
        }

        // POST: Occasions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Occasion occasion = await db.Occasions.FindAsync(id);
            db.Occasions.Remove(occasion);
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
