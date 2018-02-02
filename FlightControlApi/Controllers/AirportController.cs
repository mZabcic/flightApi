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
using FlightControlApi.Repository;
using NHibernate.Criterion;

namespace FlightControlApi.Controllers
{
    public class AirportController : ApiController
    {

        IRepository<Airport> repo;
        

        public AirportController()
        {

            repo = new Repository<Airport>();
        }


        [HttpGet]
        [Route("airport")]
        public IEnumerable<Airport> Get()
        {
            IEnumerable<Airport> airports = repo.FindAll();
            return airports;
        }

        [HttpGet]
        [Route("airport/{id}")]
        public IHttpActionResult Get(Int64 id)
        {
            Airport airport = repo.GetById(id);
            
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
            var criteria = NHibernate.Criterion.DetachedCriteria.For<Airport>()
           .Add(Restrictions.Eq("CountryId", id));
            IEnumerable<Airport> airports = repo.FindByCriteria(criteria);
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

            airport = repo.Add(airport);
            
            return Ok(airport);
        }

        [HttpPut]
        [Route("airport/{id}")]
        public IHttpActionResult Put(Int64 id, [FromBody]Airport airport)
        {
            
            airport.Id = id;
            if (!AirportController.CheckCountry(airport.CountryId))
            {
                return BadRequest("CountryId is wrong");
            }


            bool check = repo.Update(airport, id);

            if (check)
                return Ok();
            else
                return NotFound();
        }

        [HttpDelete]
        [Route("airport/{id}")]
        public IHttpActionResult Delete(Int64 id)
        {

            var criteria = NHibernate.Criterion.DetachedCriteria.For<Route>()
            .Add(Restrictions.Or(
        Restrictions.Eq("FromId", id),
        Restrictions.Eq("DestinationId", id)));
            int count = new Repository<Route>().FindByCriteria(criteria).Count();
                if (count > 0)
                {
                    return Conflict();
                }
            
            bool check = repo.Delete(id);

            if (check)
                return Ok();
            else
                return NotFound();
            

        }

        private static bool CheckCountry(Int64 id)
        {
            var criteria = NHibernate.Criterion.DetachedCriteria.For<Country>()
             .Add(Restrictions.Eq("Id", id));

            int count = new Repository<Country>().FindByCriteria(criteria).Count();

            return count > 0;
        }


    }
}
