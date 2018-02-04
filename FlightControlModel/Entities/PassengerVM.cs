using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightControlApi.Models
{
    public class PassengerVM
    {

        public virtual Int64 Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual string Identifier { get; set; }
        public virtual Int64 CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}