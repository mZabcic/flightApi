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
    public class TicketController : ApiController
    {


        [HttpGet]
        [Route("ticket")]
        public IEnumerable<TicketVM> Get()
        {
            IEnumerable<TicketVM> tickets;
            using (ISession session = NHibernateSession.OpenSession())
            {
                tickets = session.Query<TicketVM>().ToList();
            }

            return tickets;
        }

        [HttpGet]
        [Route("ticket/{id}")]
        public IHttpActionResult Get(Int64 id)
        {
            TicketVM ticket;
            using (ISession session = NHibernateSession.OpenSession())
            {
                ticket = session.Get<TicketVM>(id);
            }
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
            IEnumerable<TicketVM> tickets;
            using (ISession session = NHibernateSession.OpenSession())
            {
                tickets = session.Query<TicketVM>().Where(p => p.FlightId == id).ToList();
            }

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
            





            using (ISession session = NHibernateSession.OpenSession())
            {
                session.Save(newTicket);
            }
            return Ok(newTicket);
        }

    

        [HttpDelete]
        [Route("ticket/{id}")]
        public IHttpActionResult Delete(Int64 id)
        {
            Ticket oldTicket;
            using (ISession session = NHibernateSession.OpenSession())
            {
                session.Transaction.Begin();
                oldTicket = session.Load<Ticket>(id);
                oldTicket.Revoked = false;
                session.Update(oldTicket);

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
            return Ok();

        }

        private static bool CheckFlight(Int64 id)
        {
            int count;
            using (ISession session = NHibernateSession.OpenSession())
            {
                count = session.Query<Flight>().Where(p => p.Id == id).Count();
            }
            return count > 0;
        }

        private static bool CheckStore(Int64 id)
        {
            int count;
            using (ISession session = NHibernateSession.OpenSession())
            {
                count = session.Query<Store>().Where(p => p.Id == id).Count();
            }
            return count > 0;
        }

        private static bool CheckPassenger(Int64 id)
        {
            int count;
            using (ISession session = NHibernateSession.OpenSession())
            {
                count = session.Query<Passenger>().Where(p => p.Id == id).Count();
            }
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
            if (ticket.FlightId == 0)
            {
                return "FlightId is required";
            }
            if (ticket.SeatClassId > 4 || ticket.SeatClassId < 1)
            {
                return "SeatId is required";
            }
            if (ticket.StoreId == 0)
            {
                return "StoreId is required";
            }
            if (ticket.PassengerId == 0)
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
