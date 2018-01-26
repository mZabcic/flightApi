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
        IRepository<RouteVM> repoVM;

        public RouteController()
        {

            repo = new Repository<Route>();
            repoVM = new Repository<RouteVM>();
        }

        [HttpGet]
        [Route("route")]
        public IEnumerable<RouteVM> Get()
        {
         
            return repoVM.FindAll();
        }

        [HttpGet]
        [Route("route/{id}")]
        public IHttpActionResult Get(Int64 id)
        {
            RouteVM route = repoVM.GetById(id);
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
            var criteria = NHibernate.Criterion.DetachedCriteria.For<Airport>()
             .Add(Restrictions.Eq("Id", id));
           
            int count = new Repository<Airport>().FindByCriteria(criteria).Count();

            return count > 0;
        }


    }
}
