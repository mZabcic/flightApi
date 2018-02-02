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
using FlightControlApi.Repository;
using NHibernate.Criterion;

namespace FlightControlApi.Controllers
{
    public class RouteController : ApiController
    {

        IRepository<Route> repo;

        public RouteController()
        {

            repo = new Repository<Route>();
        }

        [HttpGet]
        [Route("route")]
        public IEnumerable<Route> Get()
        {
         
            return repo.FindAll();
        }

        [HttpGet]
        [Route("route/{id}")]
        public IHttpActionResult Get(Int64 id)
        {
            Route route = repo.GetById(id);
            if (route == null)
            {
                return NotFound();
            }
            return Ok(route);
        }

        [HttpPost]
        [Route("route")]
        public IHttpActionResult Post([FromBody]Route route)
        {
            if (route.FromId == 0)
            {
                return BadRequest("FromId is required");
            }
            if (route.DestinationId == 0)
            {
                return BadRequest("DestinationId is required");
            }
            if (route.DestinationId == route.FromId)
            {
                return BadRequest("FromId and DestinationId cannot be equal");
            }
            if (!RouteController.CheckAirport(route.DestinationId))
            {
                return BadRequest("DestinationId is wrong");
            }
            if (!RouteController.CheckAirport(route.FromId))
            {
                return BadRequest("FromId is wrong");
            }

            route = repo.Add(route);
            return Ok(route);
        }

        [HttpPut]
        [Route("route/{id}")]
        public IHttpActionResult Put(Int64 id, [FromBody]Route route)
        {
            Route oldRoute;
            route.Id = id;
            if (!RouteController.CheckAirport(route.DestinationId))
            {
                return BadRequest("DestinationId is wrong");
            }
            if (!RouteController.CheckAirport(route.FromId))
            {
                return BadRequest("FromId is wrong");
            }


            bool check = repo.Update(route, id);

            if (check)
                return Ok();
            else
                return NotFound();
        }

        [HttpDelete]
        [Route("route/{id}")]
        public IHttpActionResult Delete(Int64 id)
        {

             var criteria = NHibernate.Criterion.DetachedCriteria.For<Flight>()
            .Add(
        Restrictions.Eq("RouteId", id));
            int count = new Repository<Flight>().FindByCriteria(criteria).Count();
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

        private static bool CheckAirport(Int64 id)
        {
            var criteria = NHibernate.Criterion.DetachedCriteria.For<Airport>()
             .Add(Restrictions.Eq("Id", id));
           
            int count = new Repository<Airport>().FindByCriteria(criteria).Count();

            return count > 0;
        }


    }
}
