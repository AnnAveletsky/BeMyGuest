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
    public class UsersDiscussionsController : Controller
    {
        private Entities db = new Entities();

        // GET: UsersDiscussions
        public async Task<ActionResult> Index()
        {
            var usersDiscussions = db.UsersDiscussions.Include(u => u.AspNetUser).Include(u => u.Discussion);
            return View(await usersDiscussions.ToListAsync());
        }

        // GET: UsersDiscussions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersDiscussion usersDiscussion = await db.UsersDiscussions.FindAsync(id);
            if (usersDiscussion == null)
            {
                return HttpNotFound();
            }
            return View(usersDiscussion);
        }

        // GET: UsersDiscussions/Create
        public ActionResult Create()
        {
            ViewBag.IdUser = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.IdDiscussion = new SelectList(db.Discussions, "Id", "IdUserCreate");
            return View();
        }

        // POST: UsersDiscussions/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,IdUser,IdDiscussion,DataTimeWrite,Text")] UsersDiscussion usersDiscussion)
        {
            if (ModelState.IsValid)
            {
                usersDiscussion.Id = db.UsersDiscussions.ToList().Count;
                db.UsersDiscussions.Add(usersDiscussion);
                    
                await db.SaveChangesAsync();
                return Redirect(Request.UrlReferrer.AbsolutePath);
            }

            ViewBag.IdUser = new SelectList(db.AspNetUsers, "Id", "Email", usersDiscussion.IdUser);
            ViewBag.IdDiscussion = new SelectList(db.Discussions, "Id", "IdUserCreate", usersDiscussion.IdDiscussion);
            return View(usersDiscussion);
        }

        // GET: UsersDiscussions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersDiscussion usersDiscussion = await db.UsersDiscussions.FindAsync(id);
            if (usersDiscussion == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUser = new SelectList(db.AspNetUsers, "Id", "Email", usersDiscussion.IdUser);
            ViewBag.IdDiscussion = new SelectList(db.Discussions, "Id", "IdUserCreate", usersDiscussion.IdDiscussion);
            return View(usersDiscussion);
        }

        // POST: UsersDiscussions/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,IdUser,IdDiscussion,DataTimeWrite,Text")] UsersDiscussion usersDiscussion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usersDiscussion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdUser = new SelectList(db.AspNetUsers, "Id", "Email", usersDiscussion.IdUser);
            ViewBag.IdDiscussion = new SelectList(db.Discussions, "Id", "IdUserCreate", usersDiscussion.IdDiscussion);
            return View(usersDiscussion);
        }

        // GET: UsersDiscussions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersDiscussion usersDiscussion = await db.UsersDiscussions.FindAsync(id);
            if (usersDiscussion == null)
            {
                return HttpNotFound();
            }
            return View(usersDiscussion);
        }

        // POST: UsersDiscussions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            UsersDiscussion usersDiscussion = await db.UsersDiscussions.FindAsync(id);
            db.UsersDiscussions.Remove(usersDiscussion);
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
