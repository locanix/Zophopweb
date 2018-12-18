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

                trackerslist = (from tl in db.curpos
                         join t in db.trackers on tl.TrackerID equals t.ID
                         orderby t.ID                        
                         select new Device
                         {
                             ID = t.ID,
                             DeviceIMEI = t.IMEI,
                             VehicleNumber = t.Name,                             
                             Alt = tl.Alt,
                             DataFound = tl.Time > 0 ? 1 : 0,
                             GSMInfo = tl.GSMInfo,
                             LastDataPointEpochTime = tl.Time!=null?tl.Time:0,
                             Lat = tl.Lat,
                             Lng = tl.Lng,                             
                             Speed = tl.Speed,
                             Status = tl.Status,
                             GroupName=t.servergroup
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
                                label = g.Key,
                                value = (int)g.Count(x => x.GroupName == g.Key),
                                color = ""
                            }).ToList().OrderByDescending(l => l.value).ToList();
            ViewBag.LocationsSummary = locationsSummary;
            ViewBag.TotalDevices = trackerslist.Count();
            ViewBag.TotalDevicesList = trackerslist.Where(tl=>tl.Lat!=null).ToList();
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
        [HttpGet]
        public ActionResult CalculateAverage()
        {
            if (Session["sessUser"] == null || string.IsNullOrEmpty(Session["sessUser"].ToString()))
            {
                return RedirectToAction("Index", "Home");
            }
            var Vehicles=db.trackers.ToList().OrderByDescending(t => t.Number);

           ViewData["Vehicles"] = Vehicles.ToList();
           ViewData["Minimum"] = "";
           ViewData["Average"] = "";
           ViewData["Maximum"] = "";
           ViewData["IMEI"] = "All";
           ViewData["startdate"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
           ViewData["enddate"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            return View();
        }

        [HttpPost]
        public ActionResult CalculateAverage(string beginTime, string endTime,string IMEI)
        { 
           MySql.Data.MySqlClient.MySqlConnection mycon=new MySql.Data.MySqlClient.MySqlConnection();
            MySql.Data.MySqlClient.MySqlCommand mycommand=new MySql.Data.MySqlClient.MySqlCommand();
            mycon.ConnectionString=System.Configuration.ConfigurationManager.ConnectionStrings["another"].ConnectionString;
            mycommand.Connection=mycon;
            mycon.Open();
            Int32 unixTimestamp1 = (Int32)(DateTime.ParseExact(beginTime,"dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).AddHours(-5).AddMinutes(-30).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            Int32 unixTimestamp2 = (Int32)(DateTime.ParseExact(endTime, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).AddHours(-5).AddMinutes(-30).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            if (IMEI != "All") {
                mycommand.CommandText = String.Format("select min(geoloc.forwardrecords.RecSentDiff) as MinimumReceiverToServer,max(geoloc.forwardrecords.RecSentDiff) MaximumReceiverToServer,avg(geoloc.forwardrecords.RecSentDiff) as AverageReceiverToServer from forwardrecords where ForwardTimestamp>={0} and ForwardTimestamp<={1} and IMEI = '{2}' and RecSentDiff>0", unixTimestamp1, unixTimestamp2, IMEI);
            }
            else
            {
                mycommand.CommandText = String.Format("select min(geoloc.forwardrecords.RecSentDiff) as MinimumReceiverToServer,max(geoloc.forwardrecords.RecSentDiff) MaximumReceiverToServer,avg(geoloc.forwardrecords.RecSentDiff) as AverageReceiverToServer from forwardrecords where ForwardTimestamp>={0} and ForwardTimestamp<={1}  and RecSentDiff>0", unixTimestamp1, unixTimestamp2);
            }
            MySql.Data.MySqlClient.MySqlDataAdapter mda= new MySql.Data.MySqlClient.MySqlDataAdapter(mycommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mycon.Close();
            var Vehicles = db.trackers.ToList().OrderByDescending(t => t.Number);
            ViewData["Vehicles"] = Vehicles.ToList();
            ViewData["Minimum"] = dt.Rows[0]["MinimumReceiverToServer"].ToString();
            ViewData["Average"] = dt.Rows[0]["AverageReceiverToServer"].ToString();
            ViewData["Maximum"] = dt.Rows[0]["MaximumReceiverToServer"].ToString();
            ViewData["IMEI"] = IMEI;
            ViewData["startdate"] = DateTime.ParseExact(beginTime, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture).ToString("dd/MM/yyyy HH:mm:ss");
            ViewData["enddate"] = DateTime.ParseExact(endTime, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture).ToString("dd/MM/yyyy HH:mm:ss");
            return View();
        }
	}
}