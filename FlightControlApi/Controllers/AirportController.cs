﻿using System;
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
        IRepository<AirportVM> repoVM;

        public AirportController()
        {

            repo = new Repository<Airport>();
            repoVM = new Repository<AirportVM>();
        }


        [HttpGet]
        [Route("airport")]
        public IEnumerable<AirportVM> Get()
        {
            IEnumerable<AirportVM> airports = repoVM.FindAll();
            return airports;
        }

        [HttpGet]
        [Route("airport/{id}")]
        public IHttpActionResult Get(Int64 id)
        {
            AirportVM airport = repoVM.GetById(id);
            
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
            var criteria = NHibernate.Criterion.DetachedCriteria.For<AirportVM>()
           .Add(Restrictions.Eq("CountryId", id));
            IEnumerable<AirportVM> airports = repoVM.FindByCriteria(criteria);
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

            using (ISession session = NHibernateSession.OpenSession())
            {
                int count = session.Query<Route>().Where(c => c.FromId == id || c.DestinationId == id).Count();
                if (count > 0)
                {
                    return Conflict();
                }
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
