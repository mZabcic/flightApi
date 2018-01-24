using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightControlApi.Models
{
    public class Route
    {
        public virtual Int64 Id { get; set; }
        public virtual Int64 FromId { get; set; }
        public virtual Int64 DestinationId { get; set; }
    }
}