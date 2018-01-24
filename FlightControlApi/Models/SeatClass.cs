using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightControlApi.Models
{
    public class SeatClass
    {
        public virtual Int64 Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Decimal PriceMultipler { get; set; }
       
    }
}