using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightControlApi.Models
{
    public class TicketCreator
    {
        public virtual Int64 Id { get; set; }
        public virtual Decimal Price { get; set; }
        public virtual Int64 PassengerId { get; set; }
        public virtual Int64 FlightId { get; set; }
        public virtual Int64 SeatClassId { get; set; }
        public virtual Int64 StoreId { get; set; }
    }
}