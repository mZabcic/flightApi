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
        IRepository<PassengerVM> repoVM;

        public PassengerController()
        {

            repo = new Repository<Passenger>();
            repoVM = new Repository<PassengerVM>();
        }

        public PassengerController(IRepository<Passenger> repo, IRepository<PassengerVM> repoVM )
        {

            this.repo = repo;
            this.repoVM = repoVM;
        }

        [HttpGet]
        [Route("passenger")]
        public IEnumerable<PassengerVM> Get()
        {
            IEnumerable<PassengerVM> passengers = repoVM.FindAll();

            return passengers;
        }

        [HttpGet]
        [Route("passenger/{id}")]
        public IHttpActionResult Get(Int64 id)
        {
            PassengerVM passenger = repoVM.GetById(id);
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


            passenger = repo.Add(passenger);
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
           
            passenger.Id = id;
            if (!PassengerController.CheckCountry(passenger.CountryId))
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

            var criteria = NHibernate.Criterion.DetachedCriteria.For<Ticket>()
              .Add(Restrictions.Eq("Id", id));

            int count = new Repository<Ticket>().FindByCriteria(criteria).Count();

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

        private static bool CheckCountry(Int64 id)
        {
           

            var criteria = NHibernate.Criterion.DetachedCriteria.For<Country>()
             .Add(Restrictions.Eq("Id", id));

            int count = new Repository<Country>().FindByCriteria(criteria).Count();

            return count > 0;
        }

        private static bool UserExists(String email)
        {
            
            var criteria = NHibernate.Criterion.DetachedCriteria.For<Passenger>()
             .Add(Restrictions.Eq("Email", email));

            int count = new Repository<Passenger>().FindByCriteria(criteria).Count();

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
