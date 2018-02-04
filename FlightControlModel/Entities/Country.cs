using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;

namespace FlightControlApi.Models
{
    public class Country
    {
        public virtual Int64 Id { get; set; }
        public virtual string iso { get; set; }
        public virtual string name { get; set; }
        public virtual string printable_name { get; set; }
        public virtual string iso3 { get; set; }
        public virtual int numcode { get; set; }
        public virtual HashSet<Airport> airports { get; set; }


      

    }
}