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
    public class PlaneController : ApiController
    {

        [HttpGet]
        [Route("plane")]
        public IEnumerable<Plane> Get()
        {
            IEnumerable<Plane> planes;
            using (ISession session = NHibernateSession.OpenSession())  
            {
                planes = session.Query<Plane>().ToList();
            }
            return planes;
        }

        [HttpGet]
        [Route("planes/{id}")]
        public IHttpActionResult Get(Int64 id)
        {
            Plane plane;
            using (ISession session = NHibernateSession.OpenSession())
            {
                plane = session.Get<Plane>(id); 
            }
            if (plane == null)
            {
                return NotFound();
            }
            return Ok(plane);
        }

        [HttpPost]
        [Route("plane")]
        public IHttpActionResult Post([FromBody]Plane plane)
        {
            if (plane.Model == null)
            {
                return BadRequest("Model is required");
            }
            if (plane.SerialNumber == null)
            {
                return BadRequest("SerialNumber is required");
            }
            plane.Active = 1;

            using (ISession session = NHibernateSession.OpenSession())  // Open a session to conect to the database
            {
                session.Save(plane);
            }
            return Ok(plane);
        }

        [HttpPut]
        [Route("plane/{id}")]
        public IHttpActionResult Put(Int64 id, [FromBody]Plane plane)
        {
            Plane oldPlane;
            plane.Id = id;


            using (ISession session = NHibernateSession.OpenSession())  // Open a session to conect to the database
            {
                session.Transaction.Begin();
                oldPlane = session.Load<Plane>(id);

                oldPlane = plane;

                session.Update(oldPlane);
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


            return Ok(oldPlane);
        }

        [HttpDelete]
        [Route("plane/{id}")]
        public IHttpActionResult Delete(Int64 id)
        {

            using (ISession session = NHibernateSession.OpenSession())  // Open a session to conect to the database
            {
                Plane plane = session.Get<Plane>(id);
                if (plane == null)
                {
                    return NotFound();
                }
                session.Delete(plane);

                session.Flush();
            }
            return Ok();

        }


    }
}
