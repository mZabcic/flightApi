using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightControlApi.Models
{
    public class SeatVM
    {
        public virtual Int64 Id { get; set; }
        public virtual Int64 Num { get; set; }
        public virtual Int64 PlaneId { get; set; }
        public virtual Int64 SeatClassId { get; set; }
        public virtual SeatClass SeatClass { get; set; }
        public virtual Plane Plane { get; set; }
    }
}