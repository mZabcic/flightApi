using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightControlApi.Models
{
    public class Plane
    {
        public virtual Int64 Id { get; set; }
        public virtual string Model { get; set; }
        public virtual string SerialNumber { get; set; }
        public virtual Int64 EconomyCapacity { get; set; }
        public virtual Int64 BusinessCapacity { get; set; }
        public virtual Int64 FirstClassCapacity { get; set; }
        public virtual int Active { get; set; }
    }
}