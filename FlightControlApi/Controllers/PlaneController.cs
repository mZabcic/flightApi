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
            PlaneController.CreateSeats(plane);
            return Ok(plane);
        }

        [HttpPut]
        [Route("plane/{id}")]
        public IHttpActionResult Put(Int64 id, [FromBody]Plane plane)
        {
          

            using (ISession session = NHibernateSession.OpenSession())  // Open a session to conect to the database
            {
                var query = "update Plane set Model = :model, SerialNumber = :serial where Id = :plane";
                var update = session.CreateQuery(query)
                                    .SetParameter("plane", id)
                                     .SetParameter("serial", plane.SerialNumber)
                                      .SetParameter("model", plane.Model);
                update.ExecuteUpdate();
            }


            return Ok();
        }

        [HttpDelete]
        [Route("plane/{id}")]
        public IHttpActionResult Delete(Int64 id)
        {
            Plane oldPlane;
            using (ISession session = NHibernateSession.OpenSession())  // Open a session to conect to the database
            {
                session.Transaction.Begin();
                oldPlane = session.Load<Plane>(id);
                oldPlane.Active = 0;

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
            return Ok();

        }

        private static void CreateSeats(Plane plane)
        {
            using (ISession session = NHibernateSession.OpenSession()) { 
               Int64 curNum = 1;
            for (Int64 i = 0; i < plane.FirstClassCapacity; i++)
            {
                Seat seat = new Seat { Num = curNum, PlaneId = plane.Id, SeatClassId = 3 };
                    session.Save(seat);
                    curNum++;
            }
            for (Int64 i = 0; i < plane.BusinessCapacity; i++)
            {
                Seat seat = new Seat { Num = curNum, PlaneId = plane.Id, SeatClassId = 2 };
                    session.Save(seat);
                    curNum++;
            }
            for (Int64 i = 0; i < plane.EconomyCapacity; i++)
            {
                Seat seat = new Seat { Num = curNum, PlaneId = plane.Id, SeatClassId = 1 };
                    session.Save(seat);
                    curNum++;
            }
        }
        }


        private static void CancelFlight(Plane plane)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                var query = "update Flight set Active = false where PlaneId = :plane";
                var update = session.CreateQuery(query)
                                    .SetParameter("plane", plane.Id);
                update.ExecuteUpdate();
            }
  
        }


    }
}
