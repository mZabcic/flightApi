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

        public bool CheckFlightId()
        {
            if (this.FlightId == 0)
                return true;
            else
                return false;
        }

        public bool CheckPassengerId()
        {
            if (this.PassengerId == 0)
                return true;
            else
                return false;
        }

        public bool CheckStoreId()
        {
            if (this.StoreId == 0)
                return true;
            else
                return false;
        }

        public bool CheckSeatClass()
        {
            if (this.SeatClassId > 3 || this.SeatClassId < 1)
                return true;
            else
                return false;
        }




    }
}