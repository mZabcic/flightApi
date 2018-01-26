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
    public class FlightController : ApiController
    {

        IRepository<Flight> repo;
        IRepository<FlightVM> repoVM;
        IRepository<FlightWithTickets> repoTickets;

        public FlightController()
        {

            repo = new Repository<Flight>();
            repoVM = new Repository<FlightVM>();
            repoTickets = new Repository<FlightWithTickets>();
        }

        [HttpGet]
        [Route("flight")]
        public IEnumerable<FlightVM> Get()
        {
            IEnumerable<FlightVM> flights = repoVM.FindAll();
            
            return flights;
        }

        [HttpGet]
        [Route("flight/{id}")]
        public IHttpActionResult Get(Int64 id)
        {
            FlightWithTickets flight = repoTickets.GetById(id);
            
            if (flight == null)
            {
                return NotFound();
            }
            return Ok(flight);
        }

        //Kreiranje leta prema već postojecoj ruti
        [HttpPost]
        [Route("flight/routes")]
        public IHttpActionResult Post([FromBody]Flight flight)
        {

            String check = FlightController.checkFlight(flight, 0);
            if (!check.Equals(""))
            {
                return BadRequest(check);
            }
            flight.Price = Decimal.Round(flight.Price, 2);
            flight = repo.Add(flight);
            return Ok(flight);
        }


        [HttpPost]
        [Route("flight")]
        public IHttpActionResult PostByAirport([FromBody]FlightCreator flight)
        {
            
            if (!FlightController.CheckAirport(flight.DepAirport))
            {
                return BadRequest("DepAirport wrong");
            }
            if (!FlightController.CheckAirport(flight.ArrAirport))
            {
                return BadRequest("ArrAirport wrong");
            }

            Flight newFlight = FlightController.CreateFlight(flight);
      

            String check = FlightController.checkFlight(newFlight, 0);
            if (!check.Equals(""))
            {
                return BadRequest(check);
            }
            newFlight = repo.Add(newFlight);
            return Ok(newFlight);
        }

        [HttpPut]
        [Route("flight/{id}")]
        public IHttpActionResult Put(Int64 id, [FromBody]FlightCreator flight)
        {
         
            if (!FlightController.CheckAirport(flight.DepAirport))
            {
                return BadRequest("DepAirport wrong");
            }
            if (!FlightController.CheckAirport(flight.ArrAirport))
            {
                return BadRequest("ArrAirport wrong");
            }

            Flight newFlight = FlightController.CreateFlight(flight);
            


            
            newFlight.Id = id;

            String check = FlightController.checkFlight(newFlight, id);
            if (!check.Equals(""))
            {
                return BadRequest(check);
            }

            bool check1 = repo.Update(newFlight, id);

            if (check1)
                return Ok();
            else
                return NotFound();
        }


        [HttpPut]
        [Route("flight/route/{id}")]
        public IHttpActionResult PutRoute(Int64 id, [FromBody]Flight flight)
        {
            Flight oldFlight;
            flight.Id = id;

            String check = FlightController.checkFlight(flight, id);
            if (!check.Equals(""))
            {
                return BadRequest(check);
            }
            flight.Price = Decimal.Round(flight.Price, 2);
            bool check1 = repo.Update(flight, id);

            if (check1)
                return Ok();
            else
                return NotFound();
        }

        [HttpDelete]
        [Route("flight/{id}")]
        public IHttpActionResult Delete(Int64 id)
        {
            Flight flight = repo.GetById(id);
                flight.Canceled = true;

                

            
            bool check1 = repo.Update(flight, id);

            if (check1)
                return Ok();
            else
                return NotFound();

        }

        private static bool CheckRoute(Int64 id)
        {
      
           
            var criteria = NHibernate.Criterion.DetachedCriteria.For<Route>()
             .Add(Restrictions.Eq("Id", id));

            int count = new Repository<Route>().FindByCriteria(criteria).Count();

            return count > 0;
            
        }

        private static bool CheckPlane(Int64 id)
        {

            var criteria = NHibernate.Criterion.DetachedCriteria.For<Plane>()
             .Add(Restrictions.Eq("Id", id));

            int count = new Repository<Plane>().FindByCriteria(criteria).Count();

            return count > 0;
        }

        private static bool CheckPilot(Int64 id)
        {
           

            var criteria = NHibernate.Criterion.DetachedCriteria.For<Pilot>()
             .Add(Restrictions.Eq("Id", id));

            int count = new Repository<Pilot>().FindByCriteria(criteria).Count();

            return count > 0;
        }

        private static bool CheckPilotTime(Int64 id, DateTime from, DateTime until, Int64 flightId)
        {
            int onFlight;
            from = from.AddHours(-24);
            until = until.AddHours(24);
            using (ISession session = NHibernateSession.OpenSession())
            {
                onFlight = session.Query<Flight>().Where(p => p.PilotId == id && p.Id != flightId && p.Canceled == false  && ((p.DepTime >= from && p.DepTime <= until) || (p.ArrTime > from && p.ArrTime < until)) ).Count();
                
            }
            return onFlight > 0;
            
        }

        private static bool CheckPlaneTime(Int64 id, DateTime from, DateTime until, Int64 flightId)
        {
            int onFlight;
            from = from.AddHours(24);
            until = until.AddHours(-24);
            using (ISession session = NHibernateSession.OpenSession())
            {
                onFlight = session.Query<Flight>().Where(p => p.PlaneId == id &&  p.Id != flightId && p.Canceled == false && ((p.DepTime > from && p.DepTime < until) || (p.ArrTime > from && p.ArrTime < until))).Count();

            }
            return onFlight > 0;

        }

        private static string checkFlight(Flight flight, Int64 id)
        {
            if (flight.PilotId == 0)
            {
                return "PilotId is required";
            }
            if (flight.PlaneId == 0)
            {
                return "PlaneId is required";
            }
            if (flight.RouteId == 0)
            {
                return "RouteId is required";
            }
            if (flight.DepTime.ToString() == "1.1.0001. 0:00:00")
            {
                return "DepTime is required";
            }
            if (flight.ArrTime.ToString() == "1.1.0001. 0:00:00")
            {
                return "ArrTime is required";
            }
            if (flight.ArrTime <= flight.DepTime)
            {
                return "ArrTime is smaller than DepTime";
            }
            if (!FlightController.CheckRoute(flight.RouteId))
            {
                return "RouteId is wrong";
            }
            if (!FlightController.CheckPilot(flight.PilotId))
            {
                return "PilotId is wrong";
            }
            if (!FlightController.CheckPlane(flight.PlaneId))
            {
                return "PlaneId is wrong";
            }
            if (FlightController.CheckPilotTime(flight.PilotId, flight.DepTime, flight.ArrTime, id))
            {
                return "Pilot is busy";
            }
            if (FlightController.CheckPlaneTime(flight.PlaneId, flight.DepTime, flight.ArrTime, id))
            {
                return "Plane is busy";
            }
            return "";
        }

        private static bool CheckAirport(Int64 id)
        {
           
            var criteria = NHibernate.Criterion.DetachedCriteria.For<Airport>()
             .Add(Restrictions.Eq("Id", id));

            int count = new Repository<Airport>().FindByCriteria(criteria).Count();

            return count > 0;

        }

        private static Int64 CreateOrGetRoute(Int64 DepAirport, Int64 ArrAirport)
        {

            var criteria = NHibernate.Criterion.DetachedCriteria.For<Route>()
            .Add(Restrictions.Or(
        Restrictions.Eq("FromId", DepAirport),
        Restrictions.Eq("DestinationId", ArrAirport)));
            Route route = new Repository<Route>().FindByCriteria(criteria).First();
           
            if (route == null)
            {
                route = new Route();
                route.DestinationId = ArrAirport;
                route.FromId = DepAirport;
                IRepository<Route> routeRepo = new Repository<Route>();
                routeRepo.Add(route);
                
                return route.Id;
            } else
            {
                return route.Id;
            }
        }


        private static Flight CreateFlight(FlightCreator flight)
        {
     

            Int64 routeId = FlightController.CreateOrGetRoute(flight.DepAirport, flight.ArrAirport);
            Flight newFlight = new Flight
            {
                ArrTime = flight.ArrTime,
                DepTime = flight.DepTime,
                PlaneId = flight.PlaneId,
                PilotId = flight.PilotId,
                RouteId = routeId,
                Canceled = false,
                Price = Decimal.Round(flight.Price, 2)
            };

            return newFlight;
        }



    }
}
