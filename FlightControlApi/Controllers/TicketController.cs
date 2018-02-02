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
    public class TicketController : ApiController
    {


        IRepository<Ticket> repo;
        IRepository<TicketVM> repoVM;

        public TicketController()
        {

            repo = new Repository<Ticket>();
            repoVM = new Repository<TicketVM>();
        }

        [HttpGet]
        [Route("ticket")]
        public IEnumerable<TicketVM> Get()
        {
 

            return repoVM.FindAll();
        }

        [HttpGet]
        [Route("ticket/{id}")]
        public IHttpActionResult Get(Int64 id)
        {
            TicketVM ticket = repoVM.GetById(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }


        [HttpGet]
        [Route("ticket/flight/{id}")]
        public IHttpActionResult GetByFlight(Int64 id)
        {
            var criteria = NHibernate.Criterion.DetachedCriteria.For<TicketVM>()
             .Add(Restrictions.Eq("FlightId", id));
            IEnumerable<TicketVM> tickets = repoVM.FindByCriteria(criteria);

            return Ok(tickets);
        }

        [HttpPost]
        [Route("ticket")]
        public IHttpActionResult Post([FromBody]TicketCreator ticket)
        {
            
            String check = TicketController.CheckTicket(ticket);
            if (!check.Equals(""))
            {
                return BadRequest(check);
            }

            Int64 seatId = TicketController.CheckAvailable(ticket.FlightId, ticket.SeatClassId);

            if (seatId == 0)
            {
                return BadRequest("No more seats for wanted class");
            }
            Ticket newTicket = new Ticket { PassengerId = ticket.PassengerId, FlightId = ticket.FlightId, Revoked = false, SeatId = seatId, StoreId = ticket.StoreId, Price = 0 };






            newTicket = repo.Add(newTicket);
            return Ok(newTicket);
        }

    

        [HttpDelete]
        [Route("ticket/{id}")]
        public IHttpActionResult Delete(Int64 id)
        {
            Ticket ticket = repo.GetById(id);
            ticket.Revoked = true;
            bool check = repo.Update(ticket, id);

            if (check)
                return Ok();
            else
                return NotFound();

        }

        private static bool CheckFlight(Int64 id)
        {
            

            var criteria = NHibernate.Criterion.DetachedCriteria.For<Flight>()
             .Add(Restrictions.Eq("Id", id));

            int count = new Repository<Flight>().FindByCriteria(criteria).Count();

            return count > 0;
        }

        private static bool CheckStore(Int64 id)
        {
            var criteria = NHibernate.Criterion.DetachedCriteria.For<Store>()
             .Add(Restrictions.Eq("Id", id));

            int count = new Repository<Store>().FindByCriteria(criteria).Count();

            return count > 0;
        }

        private static bool CheckPassenger(Int64 id)
        {
            var criteria = NHibernate.Criterion.DetachedCriteria.For<Passenger>()
             .Add(Restrictions.Eq("Id", id));

            int count = new Repository<Passenger>().FindByCriteria(criteria).Count();

            return count > 0;
        }

        private static Int64 CheckAvailable(Int64 flightId, Int64 seatClassId)
        {
            FlightVM flight;
            IEnumerable<TicketVM> tickets;
            IEnumerable<Seat> seats;
            using (ISession session = NHibernateSession.OpenSession())
            {
                flight = session.Get<FlightVM>(flightId);
                tickets = session.Query<TicketVM>().Where(p => p.FlightId == flightId && p.Seat.SeatClassId == seatClassId && p.Revoked == false).OrderBy(p => p.Seat.Num).ToList();
                seats = session.Query<Seat>().Where(p => p.PlaneId == flight.PlaneId && p.SeatClassId == seatClassId).ToList();
            }
            Int64 Seats = 0;
            if (seatClassId == 1)
            {
                Seats = flight.Plane.EconomyCapacity;
            } else if (seatClassId == 2)
            {
                Seats = flight.Plane.BusinessCapacity;
            } else
            {
                Seats = flight.Plane.FirstClassCapacity;
            }
            if (tickets.Count() >= Seats)
            {
                return 0;
            }  else
            {
                Int64 seatId = 0;
                seats.ForEach(p =>
                {
                    bool found = false;
                    tickets.ForEach(r =>
                    {
                        if (r.SeatId == p.Id)
                        {
                            found = true;
                            return;
                        }
                    });

                    if (!found)
                    {
                        seatId = p.Id;
                        return;
                    }
                });

               
                
                return seatId;
            }

         

        }


        private static string CheckTicket(TicketCreator ticket)
        {
            if (ticket.CheckFlightId())
            {
                return "FlightId is required";
            }
            if (ticket.CheckSeatClass())
            {
                return "SeatClass is required";
            }
            if (ticket.CheckStoreId())
            {
                return "StoreId is required";
            }
            if (ticket.CheckPassengerId())
            {
                return "PassengerId is required";
            }
            if (!TicketController.CheckFlight(ticket.FlightId))
            {
                return "FlightId is wrong";
            }
            if (!TicketController.CheckStore(ticket.StoreId))
            {
                return "StoreId is wrong";
            }
            if (!TicketController.CheckPassenger(ticket.PassengerId))
            {
                return "PassengerId is wrong";
            }

            return "";
        }




    }
}
