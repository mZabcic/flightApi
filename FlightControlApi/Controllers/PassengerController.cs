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
    public class PassengerController : ApiController
    {


        [HttpGet]
        [Route("passenger")]
        public IEnumerable<PassengerVM> Get()
        {
            IEnumerable<PassengerVM> passengers;
            using (ISession session = NHibernateSession.OpenSession())
            {
                passengers = session.Query<PassengerVM>().ToList();
            }

            return passengers;
        }

        [HttpGet]
        [Route("passenger/{id}")]
        public IHttpActionResult Get(Int64 id)
        {
            PassengerVM passenger;
            using (ISession session = NHibernateSession.OpenSession())
            {
                passenger = session.Get<PassengerVM>(id);
            }
            if (passenger == null)
            {
                return NotFound();
            }
            return Ok(passenger);
        }


     

        [HttpPost]
        [Route("passenger")]
        public IHttpActionResult Post([FromBody]Passenger passenger)
        {

           
            if (passenger.Email == null)
            {
                return BadRequest("Email is required");
            }
            if (!PassengerController.IsValidEmail(passenger.Email))
            {
                return BadRequest("Email format is wrong");
            }
            if (passenger.Email == null)
            {
                return BadRequest("Email is required");
            }
            if (passenger.Identifier == null)
            {
                return BadRequest("Identifier is required");
            }
            if (passenger.CountryId == 0)
            {
                return BadRequest("CountryId is required");
            }
            if (!PassengerController.CheckCountry(passenger.CountryId))
            {
                return BadRequest("CountryId is wrong");
            }
            if (PassengerController.UserExists(passenger.Email))
            {
                return BadRequest("User with this email already exists");
            }


            using (ISession session = NHibernateSession.OpenSession())
            {
                session.Save(passenger);
            }
            return Ok(passenger);
        }

        [HttpPut]
        [Route("passenger/{id}")]
        public IHttpActionResult Put(Int64 id, [FromBody]Passenger passenger)
        {
            if (PassengerController.UserExists(passenger.Email))
            {
                return BadRequest("User with this email already exists");
            }
            Passenger oldPassenger;
            passenger.Id = id;
            if (!PassengerController.CheckCountry(passenger.CountryId))
            {
                return BadRequest("CountryId is wrong");
            }


            using (ISession session = NHibernateSession.OpenSession())
            {
                session.Transaction.Begin();
                oldPassenger = session.Load<Passenger>(id);
                oldPassenger = passenger;
                session.Update(oldPassenger);

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


            return Ok(oldPassenger);
        }

        [HttpDelete]
        [Route("passenger/{id}")]
        public IHttpActionResult Delete(Int64 id)
        {

            using (ISession session = NHibernateSession.OpenSession())
            {
                  int count = session.Query<Ticket>().Where(c => c.PassengerId == id).Count();
                   if (count > 0)
                   {
                       return Conflict();
                   } 
                Passenger passenger = session.Get<Passenger>(id);
                if (passenger == null)
                {
                    return NotFound();
                }
                session.Delete(passenger);

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

        private static bool UserExists(String email)
        {
            int count;
            using (ISession session = NHibernateSession.OpenSession())
            {
                count = session.Query<Passenger>().Where(p => p.Email.Equals(email)).Count();
            }
            return count > 0;
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


    }
}
