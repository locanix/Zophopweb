//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZophopWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class trackerlastlocation
    {
        public long TrackerID { get; set; }
        public long IMEI { get; set; }
        public string Name { get; set; }
        public string Servergroup { get; set; }
        public long Time { get; set; }
        public float Lng { get; set; }
        public float Lat { get; set; }
        public long Status { get; set; }
        public int Speed { get; set; }
        public short Alt { get; set; }
        public decimal GSMInfo { get; set; }
        public long Send { get; set; }
    }
}
