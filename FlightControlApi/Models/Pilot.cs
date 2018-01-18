using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightControlApi.Models
{
    public class Pilot
    {
        public virtual Int64 Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual DateTime BirthDay { get; set; }
        public virtual bool Active { get; set; }
    }
}