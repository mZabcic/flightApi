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
using NHibernate.Spatial.Type;
using NetTopologySuite.Geometries;
using NHibernate.Exceptions;

namespace FlightControlApi.Controllers
{
    public class AirportController : ApiController
    {


        [HttpGet]
        [Route("airport")]
        public IEnumerable<AirportVM> Get()
        {
            IEnumerable<AirportVM> airports;
            using (ISession session = NHibernateSession.OpenSession())
            {
                airports = session.Query<AirportVM>().ToList();
            }

            return airports;
        }

        [HttpGet]
        [Route("airport/{id}")]
        public IHttpActionResult Get(Int64 id)
        {
            AirportVM airport;
            using (ISession session = NHibernateSession.OpenSession())
            {
                airport = session.Get<AirportVM>(id);
            }
            if (airport == null)
            {
                return NotFound();
            }
            return Ok(airport);
        }


        [HttpGet]
        [Route("airport/country/{id}")]
        public IHttpActionResult GetByCountry(Int64 id)
        {
            IEnumerable<AirportVM> airports;
            using (ISession session = NHibernateSession.OpenSession())
            {
                airports = session.Query<AirportVM>().Where(p => p.CountryId == id).ToList();
            }

            return Ok(airports);
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
            if (!AirportController.CheckCountry(airport.CountryId))
            {
                return BadRequest("CountryId is wrong");
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
            if (!AirportController.CheckCountry(airport.CountryId))
            {
                return BadRequest("CountryId is wrong");
            }


            using (ISession session = NHibernateSession.OpenSession())
            {
                session.Transaction.Begin();
                oldAirport = session.Load<Airport>(id);
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
                int count = session.Query<Route>().Where(c => c.FromId == id || c.DestinationId == id).Count();
                if (count > 0)
                {
                    return Conflict();
                }
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

        private static bool CheckCountry(Int64 id)
        {
            int count;
            using (ISession session = NHibernateSession.OpenSession())
            {
                count = session.Query<Country>().Where(p => p.Id == id).Count();
            }
            return count > 0;
        }


    }
}
