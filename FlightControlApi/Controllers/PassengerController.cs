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
using NHibernate.Criterion;
using FlightControlApi.Repository;

namespace FlightControlApi.Controllers
{
    public class PassengerController : ApiController
    {


        IRepository<Passenger> repo;
        IRepository<Country> repoCountry;
        IRepository<Ticket> repoTicket;

        public PassengerController()
        {

            repo = new Repository<Passenger>();
            repoCountry = new Repository<Country>();
            repoTicket = new Repository<Ticket>();
        }

        public PassengerController(IRepository<Passenger> repo, IRepository<Country> repoCountry, IRepository<Ticket> repoTicket)
        {

            this.repo = repo;
            this.repoCountry = repoCountry;
            this.repoTicket = repoTicket;
        }

        [HttpGet]
        [Route("passenger")]
        public IEnumerable<Passenger> Get()
        {
            IEnumerable<Passenger> passengers = repo.FindAll();

            return passengers;
        }

        [HttpGet]
        [Route("passenger/{id}")]
        public IHttpActionResult Get(Int64 id)
        {
            Passenger passenger = repo.GetById(id);
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
                   if (!this.CheckCountry(passenger.CountryId))
                   {
                       return BadRequest("CountryId is wrong");
                   }
            if (this.UserExists(passenger.Email))
            {
                return BadRequest("User with this email already exists");
            } 


            passenger = repo.Add(passenger);
            return Ok(passenger);
        }

        [HttpPut]
        [Route("passenger/{id}")]
        public IHttpActionResult Put(Int64 id, [FromBody]Passenger passenger)
        {
            if (this.UserExists(passenger.Email))
            {
                return BadRequest("User with this email already exists");
            }
           
            passenger.Id = id;
            if (!this.CheckCountry(passenger.CountryId))
            {
                return BadRequest("CountryId is wrong");
            }


            bool check = repo.Update(passenger, id);

            if (check)
                return Ok();
            else
                return NotFound();
        }

        [HttpDelete]
        [Route("passenger/{id}")]
        public IHttpActionResult Delete(Int64 id)
        {

          

            int count = repoTicket.FindBy("PassengerId", id).Count();

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

        private  bool CheckCountry(Int64 id)
        {
           


            Country count = repoCountry.GetById(id);

            return count != null;
        }

        private  bool UserExists(String email)
        {
            
         

            int count = repo.FindBy("Email", email).Count();

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
