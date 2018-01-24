using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightControlApi.Models
{
    public class Store
    {
        public virtual Int64 Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual Int64 CountryId { get; set; }
    }
}