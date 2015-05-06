using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeMyGuest.Controllers
{
    public class AuthenticationController : Controller
    {
        //
        // GET: /Authentication/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Authentication/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Authentication/Registration

        public ActionResult Registration()
        {
            return View();
        }

        //
        // POST: /Authentication/Registration

        [HttpPost]
        public ActionResult Registration(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        //
        // GET: /Authentication/LogIn

        public ActionResult LogIn()
        {
            return View();
        }

        //
        // POST: /Authentication/LogIn

        [HttpPost]
        public ActionResult LogIn(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Authentication/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Authentication/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Authentication/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Authentication/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
