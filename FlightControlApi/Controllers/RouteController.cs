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
    public class RouteController : ApiController
    {

        [HttpGet]
        [Route("route")]
        public IEnumerable<RouteVM> Get()
        {
            IEnumerable<RouteVM> routes;
            using (ISession session = NHibernateSession.OpenSession())
            {
                routes = session.Query<RouteVM>().ToList();
            }
            return routes;
        }

        [HttpGet]
        [Route("route/{id}")]
        public IHttpActionResult Get(Int64 id)
        {
            RouteVM route;
            using (ISession session = NHibernateSession.OpenSession())
            {
                route = session.Get<RouteVM>(id);
            }
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

            using (ISession session = NHibernateSession.OpenSession())
            {
                session.Save(route);
            }
            return Ok(route);
        }

        [HttpPut]
        [Route("route/{id}")]
        public IHttpActionResult Put(Int64 id, [FromBody]Route route)
        {
            Route oldRoute;
            route.Id = id;


            using (ISession session = NHibernateSession.OpenSession())
            {
                session.Transaction.Begin();
                oldRoute = session.Load<Route>(id);

                oldRoute = route;

                session.Update(oldRoute);
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


            return Ok(oldRoute);
        }

        [HttpDelete]
        [Route("route/{id}")]
        public IHttpActionResult Delete(Int64 id)
        {

            using (ISession session = NHibernateSession.OpenSession())
            {
                Route route = session.Get<Route>(id);
                if (route == null)
                {
                    return NotFound();
                }
                session.Delete(route);

                session.Flush();
            }
            return Ok();

        }

        private static bool CheckAirport(Int64 id)
        {
            int count;
            using (ISession session = NHibernateSession.OpenSession())
            {
                count = session.Query<Airport>().Where(p => p.Id == id).Count();
            }
            return count > 0;
        }


    }
}
