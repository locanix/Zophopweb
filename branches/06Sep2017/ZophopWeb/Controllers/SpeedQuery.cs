using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Data.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZophopWeb.Models;

namespace ZophopWeb.Controllers
{
    public static class SpeedQuery
    {
        public static Func<DataContext, IQueryable<Device>>
        getDevices = CompiledQuery.Compile((DataContext db)
            => from t in db.GetTable<tracker>()
               let p = (from dp in db.GetTable<point>() where dp.TrackerID == t.ID orderby dp.Time descending select dp).FirstOrDefault()
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
               });
    }
}