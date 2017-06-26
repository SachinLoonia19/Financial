using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Financial.Models;

namespace Financial.Controllers
{
    public class SignUpController : Controller
    {
        private FinancialConnection db = new FinancialConnection();

        // GET: SignUp
        public ActionResult Index()
        {
            var firmRegistrations = db.FirmRegistrations.Include(f => f.CountryMaster).Include(f => f.StateMaster);
            return View(firmRegistrations.ToList());
        }

        // GET: SignUp/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FirmRegistration firmRegistration = db.FirmRegistrations.Find(id);
            if (firmRegistration == null)
            {
                return HttpNotFound();
            }
            return View(firmRegistration);
        }

        // GET: SignUp/Create
        public ActionResult Create()
        {
            ViewBag.CountryMasterID = new SelectList(db.CountryMasters, "ID", "Name");
            ViewBag.StateMasterID = new SelectList(db.StateMasters, "ID", "Name");
            return View(); 
        }

        // POST: SignUp/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FirmRegistration firmRegistration)
        {
            if (ModelState.IsValid)
            {
                db.FirmRegistrations.Add(firmRegistration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryMasterID = new SelectList(db.CountryMasters, "ID", "Name", firmRegistration.CountryMasterID);
            ViewBag.StateMasterID = new SelectList(db.StateMasters, "ID", "Name", firmRegistration.StateMasterID);
            return View(firmRegistration);
        }

        // GET: SignUp/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FirmRegistration firmRegistration = db.FirmRegistrations.Find(id);
            if (firmRegistration == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryMasterID = new SelectList(db.CountryMasters, "ID", "Name", firmRegistration.CountryMasterID);
            ViewBag.StateMasterID = new SelectList(db.StateMasters, "ID", "Name", firmRegistration.StateMasterID);
            return View(firmRegistration);
        }

        // POST: SignUp/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirmName,ContactPersonName,Address,Contact,Email,CountryMasterID,StateMasterID,City,DateAdded,DateModified,PinCode,SMSLimit,OTP,IsOtpUsed")] FirmRegistration firmRegistration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(firmRegistration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryMasterID = new SelectList(db.CountryMasters, "ID", "Name", firmRegistration.CountryMasterID);
            ViewBag.StateMasterID = new SelectList(db.StateMasters, "ID", "Name", firmRegistration.StateMasterID);
            return View(firmRegistration);
        }

        // GET: SignUp/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FirmRegistration firmRegistration = db.FirmRegistrations.Find(id);
            if (firmRegistration == null)
            {
                return HttpNotFound();
            }
            return View(firmRegistration);
        }

        // POST: SignUp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FirmRegistration firmRegistration = db.FirmRegistrations.Find(id);
            db.FirmRegistrations.Remove(firmRegistration);
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
