using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlightControlApi.Models;
using NHibernate;
using NHibernate.Linq;

namespace FlightControlApi.Controllers
{
    public class CountryController : ApiController
    {

        ISession session;

    
        [HttpGet]
        [Route("country")]
        public IEnumerable<Country> Get()
        {
            IEnumerable<Country> countries;
            using (ISession session = NHibernateSession.OpenSession())
            {
                countries = session.Query<Country>().ToList();
            }
            return countries;
        }

        [HttpGet]
        [Route("country/{id}")]
        public IHttpActionResult Get(Int64 id)
        {
            Country countries;
            using (ISession session = NHibernateSession.OpenSession())
            {
                countries = session.Get<Country>(id);
            }
            if (countries == null)
            {
                return NotFound();
            }
            return Ok(countries);
        }
    }
}
