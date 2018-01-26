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
using FlightControlApi.Repository;

namespace FlightControlApi.Controllers
{
    public class PlaneController : ApiController
    {

      
        IRepository<Plane> repo;

        public PlaneController()
        {
         
            repo = new Repository<Plane>();
        }

      

        [HttpGet]
        [Route("plane")]
        public IEnumerable<Plane> Get()
        {
            IEnumerable<Plane> planes;
            planes = repo.FindAll();
            return planes;
        }

        [HttpGet]
        [Route("planes/{id}")]
        public IHttpActionResult Get(Int64 id)
        {
            Plane plane = repo.GetById(id);
            
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


            repo.Add(plane);
            
            PlaneController.CreateSeats(plane);
            return Ok(plane);
        }

        [HttpPut]
        [Route("plane/{id}")]
        public IHttpActionResult Put(Int64 id, [FromBody]Plane plane)
        {


            using (ISession session = NHibernateSession.OpenSession())
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
            using (ISession session = NHibernateSession.OpenSession())
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
            IRepository<Seat> seatRepo = new Repository<Seat>();
               Int64 curNum = 1;
            for (Int64 i = 0; i < plane.FirstClassCapacity; i++)
            {
                Seat seat = new Seat { Num = curNum, PlaneId = plane.Id, SeatClassId = 3 };
                seatRepo.Add(seat);
                    curNum++;
            }
            for (Int64 i = 0; i < plane.BusinessCapacity; i++)
            {
                Seat seat = new Seat { Num = curNum, PlaneId = plane.Id, SeatClassId = 2 };
                seatRepo.Add(seat);
                curNum++;
            }
            for (Int64 i = 0; i < plane.EconomyCapacity; i++)
            {
                Seat seat = new Seat { Num = curNum, PlaneId = plane.Id, SeatClassId = 1 };
                seatRepo.Add(seat);
                curNum++;
            }
        
        }


   


    }
}
