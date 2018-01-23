using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlightControlApi.Models;
using NHibernate;
using NHibernate.Linq;
using System.Reflection;
using System.Collections;

namespace FlightControlApi.Controllers
{
    public class AirportController : ApiController
    {

        ISession session = NHibernateSession.OpenSession();

        [HttpGet]
        [Route("airport")]
        public IEnumerable<Airport> Get()
        {
            IEnumerable<Airport> airports;
           
                airports = session.Query<Airport>().ToList();
            

            return airports;
        }

        [HttpGet]
        [Route("airport/{id}")]
        public IHttpActionResult Get(Int64 id)
        {
            Airport airport;
            using (ISession session = NHibernateSession.OpenSession())
            {
                airport = session.Get<Airport>(id);
            }
            if (airport == null)
            {
                return NotFound();
            }
            return Ok(airport);
        }

        [HttpPost]
        [Route("airport")]
        public IHttpActionResult Post([FromBody]Airport airport)
        {
            if (airport.Name == null)
            {
                return BadRequest("Name is required");
            }
            if (airport.Address == null)
            {
                return BadRequest("Address is required");
            }
            if (airport.ZipCode == null)
            {
                return BadRequest("ZipCode is required");
            }
            if (airport.CountryId == 0)
            {
                return BadRequest("CountryId is required");
            }
            airport.Location = new NHibernate.Spatial.Type.MsSql2008GeographyType();
          
            if (airport.Location == null)
            {
                return BadRequest("Location is required");
            }




            using (ISession session = NHibernateSession.OpenSession())
            {
                session.Save(airport);
            }
            return Ok(airport);
        }

        [HttpPut]
        [Route("airport/{id}")]
        public IHttpActionResult Put(Int64 id, [FromBody]Airport airport)
        {
            Airport oldAirport;
            airport.Id = id;


            using (ISession session = NHibernateSession.OpenSession())
            {
                session.Transaction.Begin();
                oldAirport = session.Load<Airport>(id);
                airport.Location = oldAirport.Location;
                oldAirport = airport;

                session.Update(oldAirport);
                try
                {
                    session.Transaction.Commit();
                }
                catch (NHibernate.StaleStateException exception)
                {
                    session.Transaction.Dispose();
                    return NotFound();
                }
            }


            return Ok(oldAirport);
        }

        [HttpDelete]
        [Route("airport/{id}")]
        public IHttpActionResult Delete(Int64 id)
        {

            using (ISession session = NHibernateSession.OpenSession())
            {
                Airport airport = session.Get<Airport>(id);
                if (airport == null)
                {
                    return NotFound();
                }
                session.Delete(airport);

                session.Flush();
            }
            return Ok();

        }


    }
}
