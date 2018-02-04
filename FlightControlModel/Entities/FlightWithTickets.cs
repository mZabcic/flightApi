using System;

using System.Linq;
using System.Web;

namespace FlightControlApi.Models
{
    public class FlightWithTickets
    {
        public virtual Int64 Id { get; set; }
        public virtual Int64 RouteId { get; set; }
        public virtual Int64 PlaneId { get; set; }
        public virtual Int64 PilotId { get; set; }
        public virtual DateTime DepTime { get; set; }
        public virtual DateTime ArrTime { get; set; }
        public virtual bool Canceled { get; set; }
        public virtual Decimal Price { get; set; }
        public virtual RouteVM Route { get; set; }
        public virtual Plane Plane { get; set; }
        public virtual Pilot Pilot { get; set; }
       // public virtual Set<TicketVM> Tickets { get; set; }
    }
}