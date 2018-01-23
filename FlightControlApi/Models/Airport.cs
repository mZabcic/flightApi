using System;

using System.Linq;
using System.Web;
using NHibernate.Spatial.Type;
using Iesi.Collections.Generic;

namespace FlightControlApi.Models
{
    public class Airport
    {
        public virtual Int64 Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual Int64 CountryId { get; set; }
        public virtual MsSql2008GeographyType Location { get; set;}
        public virtual ISet<Country> Country { get; set; }

    }
}