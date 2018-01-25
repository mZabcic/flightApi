using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightControlApi.Models
{
    public class Ticket
    {
        public virtual Int64 Id { get; set; }
        public virtual Decimal Price { get; set; }
        public virtual bool Revoked { get; set; }
        public virtual Int64 SeatId { get; set; }
        public virtual Int64 FlightId { get; set; }
        public virtual Int64 StoreId { get; set; }
        public virtual Int64 PassengerId { get; set; }

    }
}