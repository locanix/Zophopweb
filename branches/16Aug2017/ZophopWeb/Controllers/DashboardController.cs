using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZophopWeb.Models;

namespace ZophopWeb.Controllers
{
    public class DashboardController : Controller
    {
        private geolocEntities db = new geolocEntities();

        //
        // GET: /Dashboard/
        public ActionResult Index()
        {
            if (Session["sessUser"] == null || string.IsNullOrEmpty(Session["sessUser"].ToString()))
            {
                return RedirectToAction("Index", "Home");
            }
            //var trackerslist = db.trackers.ToList().OrderByDescending(t => t.ID);            
            //var trackerslist = (from t in db.trackers
            //                       join p in db.points
            //                           on t.ID equals p.TrackerID into dv
            //                       from d in dv.OrderByDescending(o => o.TrackerID).Take(1)).ToList();

            var trackerslist = new List<Device>();

            try
            {
              /*  trackerslist = (from t in db.trackerlastlocations 
                                orderby t.TrackerID
                                select new Device
                                {
                                    ID = t.TrackerID,
                                    DeviceIMEI = t.IMEI,
                                    VehicleNumber = t.Name,
                                    GroupName = t.Servergroup,
                                    Alt = t.Alt,
                                    DataFound = t.Time > 0 ? 1 : 0,
                                    GSMInfo = t.GSMInfo,
                                    LastDataPointEpochTime = t.Time,
                                    Lat = t.Lat,
                                    Lng = t.Lng,
                                    Send = t.Send,
                                    Speed = t.Speed,
                                    Status = t.Status
                                }).ToList();
               */

       //Satish Start

                trackerslist = (from tl in db.trackerlastlocations
                         join t in db.trackers on tl.TrackerID equals t.ID
                         orderby t.ID
                          //      orderby tl.TrackerID
                         select new Device
                         {
                             ID = t.ID,
                             DeviceIMEI = t.IMEI,
                             VehicleNumber = tl.Name,
                             GroupName = tl.Servergroup,
                             Alt = tl.Alt,
                             DataFound = tl.Time > 0 ? 1 : 0,
                             GSMInfo = tl.GSMInfo,
                             LastDataPointEpochTime = tl.Time,
                             Lat = tl.Lat,
                             Lng = tl.Lng,
                             Send = tl.Send,
                             Speed = tl.Speed,
                             Status = tl.Status
                         }).ToList(); 

       //Satish close


    /*            trackerslist = (from t in db.trackers
                                let p = (from dp in db.points where dp.TrackerID == t.ID orderby dp.Time descending select dp).FirstOrDefault()
                                //where p != null
                                orderby t.ID descending
                                select new Device
                                {
                                    ID = t.ID,
                                    DeviceIMEI = t.IMEI,
                                    VehicleNumber = t.Name,
                                    GroupName = t.servergroup,
                                    LastDataPointEpochTime = (p != null) ? p.Time : 0,
                                    Alt = (p != null) ? p.Alt : (short)0,
                                    GSMInfo = (p != null) ? p.GSMInfo : 0,
                                    Lat = (p != null) ? p.Lat : 0,
                                    Lng = (p != null) ? p.Lng : 0,
                                    Send = (p != null) ? p.Send : 0,
                                    Speed = (p != null) ? p.speed : 0,
                                    Status = (p != null) ? p.Status : 0,
                                    DataFound = (p != null) ? 1 : 0,
                                    //LastDataPointDateTime = (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddSeconds(p.Time),
                                    //Status = ((new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddSeconds(p.Time).ToLocalTime() < DateTime.Now.AddMinutes(15)) ? "Offline" : "Online"
                                }).ToList();

     */ 
                //var fulltrackerslist = db.trackers.ToList().OrderByDescending(t => t.ID);                
                //foreach (var item in fulltrackerslist)
                //{
                //    point tdpoint = null;
                //    try
                //    {
                //        tdpoint = db.points.Where(t => t.TrackerID == item.ID).OrderByDescending(t => t.Time).Take(1).ToList().FirstOrDefault();
                //    }
                //    catch { }

                //    trackerslist.Add(new Device
                //                    {
                //                        ID = item.ID,
                //                        DeviceIMEI = item.IMEI,
                //                        VehicleNumber = item.Name,
                //                        GroupName = item.servergroup,
                //                        LastDataPointEpochTime = (tdpoint != null) ? tdpoint.Time : 0,
                //                        Alt = (tdpoint != null) ? tdpoint.Alt : (short)0,
                //                        GSMInfo = (tdpoint != null) ? tdpoint.GSMInfo : 0,
                //                        Lat = (tdpoint != null) ? tdpoint.Lat : 0,
                //                        Lng = (tdpoint != null) ? tdpoint.Lng : 0,
                //                        Send = (tdpoint != null) ? tdpoint.Send : 0,
                //                        Speed = (tdpoint != null) ? tdpoint.speed : 0,
                //                        Status = (tdpoint != null) ? tdpoint.Status : 0,
                //                        DataFound = (tdpoint != null) ? 1 : 0,

                //                    });
		 
                //}
            }
            catch(Exception ex) {
                throw ex;
            }

            //DateTime dt1 = (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).ToLocalTime();
            //DateTime dt2 = DateTime.Now.AddMinutes(-5);
            //var ttime = (dt2 - dt1).TotalSeconds;

            //try
            //{
            //    trackerslist = (from t in db.trackers
            //                    orderby t.ID descending
            //                    select new Device
            //                    {
            //                        ID = t.ID,
            //                        DeviceIMEI = t.IMEI,
            //                        VehicleNumber = t.Name,
            //                        GroupName = t.servergroup,
            //                        LastDataPointEpochTime = (long)ttime,
            //                        Alt = (short)0,
            //                        GSMInfo = 0,
            //                        Lat = 0,
            //                        Lng = 0,
            //                        Send = 0,
            //                        Speed = 0,
            //                        Status = 0,
            //                        DataFound = 0,
            //                        //LastDataPointDateTime = (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddSeconds(p.Time),
            //                        //Status = ((new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddSeconds(p.Time).ToLocalTime() < DateTime.Now.AddMinutes(15)) ? "Offline" : "Online"
            //                    }).ToList();
            //}
            //catch { }

            TextInfo myTI = new CultureInfo("en-US",false).TextInfo;

            var locationsSummary = (from p in trackerslist
                            group p by p.GroupName into g
                            select new
                            {
                                label = myTI.ToTitleCase(g.Key),
                                value = (int)g.Count(x => x.GroupName == g.Key),
                                color = ""
                            }).ToList().OrderByDescending(l => l.value).ToList();
            ViewBag.LocationsSummary = locationsSummary;
            ViewBag.TotalDevices = trackerslist.Count();
            ViewBag.TotalDevicesList = trackerslist.ToList();
            //var ngtrackerslist = trackerslist.Where(t => t.GroupName.ToLower().Equals("nagpur")).ToList();
            //ViewBag.NagpurDevices = ngtrackerslist.Count();
            //ViewBag.NagpurDevicesList = ngtrackerslist.ToList();

            //var tntrackerslist = trackerslist.Where(t => t.GroupName.ToLower().Equals("thane")).ToList();
            //ViewBag.ThaneDevices = tntrackerslist.Count();
            //ViewBag.NagpurDevicesList = ngtrackerslist.ToList();

            //var offtrackerslist = trackerslist.Where(t => t.DataFound == 1 && (t.LastDataPointEpochTime == 0 || (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddSeconds(t.LastDataPointEpochTime).ToLocalTime() < DateTime.Now.AddMinutes(-15))).ToList();
            var offtrackerslist = trackerslist.Where(t => (t.LastDataPointEpochTime == 0 || (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddSeconds(t.LastDataPointEpochTime).ToLocalTime() < DateTime.Now.AddMinutes(-15))).ToList();
            ViewBag.OfflineDevices = offtrackerslist.Count();
            ViewBag.OfflineDevicesList = offtrackerslist.ToList();

            var movingtrackerslist = trackerslist.Where(t => !offtrackerslist.Any(f => f.ID == t.ID)).Where(t => t.Speed > 0).ToList();
            ViewBag.MovingDevices = movingtrackerslist.Count();
            ViewBag.MovingDevicesList = movingtrackerslist.ToList();

            var stationarytrackerslist = trackerslist.Where(t => !offtrackerslist.Any(f => f.ID == t.ID)).Where(t => t.Speed == 0).ToList();
            ViewBag.StationaryDevices = stationarytrackerslist.Count();
            ViewBag.StationaryDevicesList = stationarytrackerslist.ToList();
            return View();
        }
	}
}