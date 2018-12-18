using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZophopWeb.Models
{
    public class Device
    {
        public long ID { get; set; }
        public Nullable<long> DeviceIMEI { get; set; }
        public string VehicleNumber { get; set; }
        public string GroupName { get; set; }
        //public string Status { get; set; }        
        //public long LastDataPointID { get; set; }
        public long LastDataPointEpochTime { get; set; }
        //public DateTime LastDataPointDateTime { get; set; }
        //public string LastDataPointDateTime { get; set; }
        public float Lng { get; set; }
        public float Lat { get; set; }
        public long Status { get; set; }
        public int Speed { get; set; }
        public short Alt { get; set; }
        public decimal GSMInfo { get; set; }
        public long Send { get; set; }

        public int DataFound { get; set; }
    }
}