using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightControlApi.Models
{
    public class TicketVM
    {
        public virtual Int64 Id { get; set; }
        public virtual Decimal Price { get; set; }
        public virtual Int64 SeatId { get; set; }
        public virtual Int64 FlightId { get; set; }
        public virtual Int64 StoreId { get; set; }
        public virtual Int64 PassengerId { get; set; }
        public virtual FlightVM Flight { get; set; }
        public virtual StoreVM Store { get; set; }
        public virtual Seat Seat { get; set; }
        public virtual PassengerVM Passenger { get; set; }
        public virtual bool Revoked { get; set; }
    }
}