using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZophopWeb.Models;

namespace ZophopWeb.Controllers
{
    public class DevicesController : Controller
    {
        private geolocEntities db = new geolocEntities();

        // GET: /Devices/
        public ActionResult Index()
        {
            if (Session["sessUser"] == null || string.IsNullOrEmpty(Session["sessUser"].ToString()))
            {
                return RedirectToAction("Index", "Home");
            }

            var trackerslist = db.trackers.ToList().OrderByDescending(t => t.ID);
            ViewBag.DevicesCount = trackerslist.Count();
            return View(trackerslist);
        }

        // GET: /Devices/Details/5
        public ActionResult Details(long? id)
        {
            if (Session["sessUser"] == null || string.IsNullOrEmpty(Session["sessUser"].ToString()))
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tracker tracker = db.trackers.Find(id);
            if (tracker == null)
            {
                return HttpNotFound();
            }
            return View(tracker);
        }

        // GET: /Devices/Tracks/5
        public ActionResult Tracks(long? id)
        {
            if (Session["sessUser"] == null || string.IsNullOrEmpty(Session["sessUser"].ToString()))
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null || id == 0)
            {
                return RedirectToAction("Index");
            }

            ViewBag.DeviceId = id;
            return View(db.points.Where(t => t.TrackerID == id).OrderByDescending(t => t.Time).Take(20).ToList());
        }

        // GET: /Devices/Create
        public ActionResult Create()
        {
            if (Session["sessUser"] == null || string.IsNullOrEmpty(Session["sessUser"].ToString()))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: /Devices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tracker tracker)
        {
            if (Session["sessUser"] == null || string.IsNullOrEmpty(Session["sessUser"].ToString()))
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrEmpty(tracker.Name) && tracker.IMEI != null)
                    {
                        tracker newtracker = new tracker();
                        newtracker.UserID = 1;
                        newtracker.Name = tracker.Name;
                        newtracker.IMEI = tracker.IMEI;
                        newtracker.Comment = string.IsNullOrEmpty(tracker.Comment) == true ? "" : tracker.Comment;
                        newtracker.servergroup = string.IsNullOrEmpty(tracker.servergroup) == true ? "" : tracker.servergroup;

                        newtracker.IconID = 1;
                        newtracker.HistoryColor = "#ff0000";
                        newtracker.Period = 60;
                        newtracker.SleepPeriod = 600;
                        newtracker.Phone = "";
                        newtracker.ParkRadius = 50;
                        newtracker.MinParkTime = 300;
                        newtracker.DaysToStore = 60;
                        newtracker.Enable = true;
                        newtracker.FuelExpenseHr = 0;
                        newtracker.Number = 10000;
                        newtracker.MaxSpeed = 110;
                        newtracker.AlarmParkTime = 1800;
                        newtracker.DeviceType = "";
                        newtracker.CreateDate = 0;
                        newtracker.InstallDate = 0;
                        newtracker.StateNumber = "";
                        newtracker.InstallerName = "";
                        newtracker.MinIdleTime = 180;
                        newtracker.MinDrain = 0;
                        newtracker.Flags = "";

                        db.trackers.Add(newtracker);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Message = "Please enter Device IMEI and Vehicle Number!";
                    }
                }
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
            }

            return View(tracker);
        }

        // GET: /Devices/Edit/5
        public ActionResult Edit(long? id)
        {
            if (Session["sessUser"] == null || string.IsNullOrEmpty(Session["sessUser"].ToString()))
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tracker tracker = db.trackers.Find(id);
            if (tracker == null)
            {
                return HttpNotFound();
            }
            return View(tracker);
        }

        // POST: /Devices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tracker tracker)
        {
            if (Session["sessUser"] == null || string.IsNullOrEmpty(Session["sessUser"].ToString()))
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    tracker edittracker = db.trackers.Find(tracker.ID);
                    if (edittracker == null)
                    {
                        return HttpNotFound();
                    }
                    if (!string.IsNullOrEmpty(tracker.Name) && tracker.IMEI != null)
                    {
                        edittracker.Name = tracker.Name;
                        edittracker.IMEI = tracker.IMEI;
                        edittracker.Comment = string.IsNullOrEmpty(tracker.Comment) == true ? "" : tracker.Comment;
                        edittracker.servergroup = string.IsNullOrEmpty(tracker.servergroup) == true ? "" : tracker.servergroup;

                        db.Entry(edittracker).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Message = "Please enter Device IMEI and Vehicle Number!";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View(tracker);
        }

        // GET: /Devices/Delete/5
        public ActionResult Delete(long? id)
        {
            if (Session["sessUser"] == null || string.IsNullOrEmpty(Session["sessUser"].ToString()))
            {
                return RedirectToAction("Index", "Home");
            }
            if (!Session["sessUser"].ToString().Equals("admin"))
            {
                return RedirectToAction("Index", "Devices");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tracker tracker = db.trackers.Find(id);
            if (tracker == null)
            {
                return HttpNotFound();
            }
            return View(tracker);
        }

        // POST: /Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            if (Session["sessUser"] == null || string.IsNullOrEmpty(Session["sessUser"].ToString()))
            {
                return RedirectToAction("Index", "Home");
            }
            if (!Session["sessUser"].ToString().Equals("admin"))
            {
                return RedirectToAction("Index", "Devices");
            }

            tracker tracker = db.trackers.Find(id);
            db.trackers.Remove(tracker);
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
