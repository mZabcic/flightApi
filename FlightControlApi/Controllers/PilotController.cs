using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlightControlApi.Models;
using NHibernate;
using System.Reflection;
using System.Collections;

namespace FlightControlApi.Controllers
{
    public class PilotController : ApiController
    {
        
        [HttpGet]
        [Route("pilot")]
        public IEnumerable<Pilot> Get()
        {
            IEnumerable<Pilot> pilots;
            using (ISession session = NHibernateSession.OpenSession())  // Open a session to conect to the database
            {
                pilots = session.Query<Pilot>().ToList(); //  Querying to get all the books
            }
            return pilots;
        }

        [HttpGet]
        [Route("pilot/{id}")]
        public IHttpActionResult Get(Int64 id)
        {
            Pilot pilot;
            using (ISession session = NHibernateSession.OpenSession())  // Open a session to conect to the database
            {
                pilot = session.Get<Pilot>(id); //  Querying to get all the books
            }
            if (pilot == null)
            {
                return NotFound();
            }
            return Ok(pilot);
        }

        [HttpPost]
        [Route("pilot")]
        public IHttpActionResult Post([FromBody]Pilot pilot)
        {
            if (pilot.FirstName == null)
            {
                return BadRequest("FirstName is required");
            }
            if (pilot.LastName == null)
            {
                return BadRequest("LastName is required");
            }
            if (pilot.BirthDay.ToString() == "1.1.0001. 0:00:00")
            {
                return BadRequest("BirthDay is required");
            }
            pilot.Active = true;
           
            using (ISession session = NHibernateSession.OpenSession())  // Open a session to conect to the database
            {
                session.Save(pilot);
            }
            return Ok(pilot);
        }

        [HttpDelete]
        [Route("pilot/{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete]
        [Route("pilot/{id}")]
        public IHttpActionResult Delete(Int64 id)
        {
            
            using (ISession session = NHibernateSession.OpenSession())  // Open a session to conect to the database
            {
                Pilot pilot = session.Get<Pilot>(id);
                
                session.Delete(pilot);

                session.Flush();
            }
            return Ok();

        }

       
    }
}
