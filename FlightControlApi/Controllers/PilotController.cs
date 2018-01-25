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
    public class PilotController : ApiController
    {
        
        [HttpGet]
        [Route("pilot")]
        public IEnumerable<Pilot> Get()
        {
            IEnumerable<Pilot> pilots;
            using (ISession session = NHibernateSession.OpenSession())  
            {
                pilots = session.Query<Pilot>().ToList(); 
            }
            return pilots;
        }

        [HttpGet]
        [Route("pilot/{id}")]
        public IHttpActionResult Get(Int64 id)
        {
            Pilot pilot;
            using (ISession session = NHibernateSession.OpenSession())  
            {
                pilot = session.Get<Pilot>(id); 
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
           
            using (ISession session = NHibernateSession.OpenSession())  
            {
                session.Save(pilot);
            }
            return Ok(pilot);
        }

        [HttpPut]
        [Route("pilot/{id}")]
        public IHttpActionResult Put(Int64 id, [FromBody]Pilot pilot)
        {
            Pilot oldPilot;
            pilot.Id = id;
          

                using (ISession session = NHibernateSession.OpenSession())  
            {
                session.Transaction.Begin();
                 oldPilot = session.Load<Pilot>(id);
                
                oldPilot = pilot;

                session.Update(oldPilot);
                try
                {
                    session.Transaction.Commit();
                } catch (NHibernate.StaleStateException exception)
                {
                    session.Transaction.Dispose();
                    return NotFound();
                }
            }
            

            return Ok(oldPilot);
        }

        [HttpDelete]
        [Route("pilot/{id}")]
        public IHttpActionResult Delete(Int64 id)
        {
            Pilot pilot;
            using (ISession session = NHibernateSession.OpenSession())  
            {
                session.Transaction.Begin();
                pilot = session.Load<Pilot>(id);
                pilot.Active = false;

                session.Update(pilot);
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

       
    }
}
