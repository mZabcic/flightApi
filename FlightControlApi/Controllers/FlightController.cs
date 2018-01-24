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
    public class FlightController : ApiController
    {

        [HttpGet]
        [Route("flight")]
        public IEnumerable<FlightVM> Get()
        {
            IEnumerable<FlightVM> flights;
            using (ISession session = NHibernateSession.OpenSession())
            {
                flights = session.Query<FlightVM>().ToList();
            }
            return flights;
        }

        [HttpGet]
        [Route("flight/{id}")]
        public IHttpActionResult Get(Int64 id)
        {
            FlightVM flight;
            using (ISession session = NHibernateSession.OpenSession())
            {
                flight = session.Get<FlightVM>(id);
            }
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
            using (ISession session = NHibernateSession.OpenSession())
            {
                session.Save(flight);
            }
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
            using (ISession session = NHibernateSession.OpenSession())
            {
                session.Save(newFlight);
            }
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
            


            Flight oldFlight;
            newFlight.Id = id;

            String check = FlightController.checkFlight(newFlight, id);
            if (!check.Equals(""))
            {
                return BadRequest(check);
            }

            using (ISession session = NHibernateSession.OpenSession())
            {
                session.Transaction.Begin();
                oldFlight = session.Load<Flight>(id);

                oldFlight = newFlight;

                session.Update(oldFlight);
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


            return Ok(oldFlight);
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
            using (ISession session = NHibernateSession.OpenSession())
            {
                session.Transaction.Begin();
                oldFlight = session.Load<Flight>(id);

                oldFlight = flight;

                session.Update(oldFlight);
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


            return Ok(oldFlight);
        }

        [HttpDelete]
        [Route("flight/{id}")]
        public IHttpActionResult Delete(Int64 id)
        {

            using (ISession session = NHibernateSession.OpenSession())
            {
                Flight flight = session.Get<Flight>(id);
                if (flight == null)
                {
                    return NotFound();
                }
                session.Delete(flight);

                session.Flush();
            }
            return Ok();

        }

        private static bool CheckRoute(Int64 id)
        {
            int count;
            using (ISession session = NHibernateSession.OpenSession())
            {
                count = session.Query<Route>().Where(p => p.Id == id).Count();
            }
            return count > 0;
        }

        private static bool CheckPlane(Int64 id)
        {
            int count;
            using (ISession session = NHibernateSession.OpenSession())
            {
                count = session.Query<Plane>().Where(p => p.Id == id).Count();
            }
            return count > 0;
        }

        private static bool CheckPilot(Int64 id)
        {
            int count;
            using (ISession session = NHibernateSession.OpenSession())
            {
                count = session.Query<Pilot>().Where(p => p.Id == id && p.Active).Count();
            }
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
            int count;
            using (ISession session = NHibernateSession.OpenSession())
            {
                count = session.Query<Airport>().Where(p => p.Id == id).Count();
            }
            return count > 0;
        }

        private static Int64 CreateOrGetRoute(Int64 DepAirport, Int64 ArrAirport)
        {
            Route route;
            using (ISession session = NHibernateSession.OpenSession())
            {
                route = session.Query<Route>().Where(p => p.FromId == DepAirport && p.DestinationId == ArrAirport).FirstOrDefault();

            }
            if (route == null)
            {
                route = new Route();
                route.DestinationId = ArrAirport;
                route.FromId = DepAirport;
                using (ISession session = NHibernateSession.OpenSession())
                {
                    session.Save(route);
                }
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
