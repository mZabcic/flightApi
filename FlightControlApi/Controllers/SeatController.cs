using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlightControlApi.Models;
using NHibernate;
using NHibernate.Linq;

namespace FlightControlApi.Controllers
{
    public class SeatController : ApiController
    {

        ISession session;


        [HttpGet]
        [Route("seat/class")]
        public IEnumerable<SeatClass> Get()
        {
            IEnumerable<SeatClass> seatClasses;
            using (ISession session = NHibernateSession.OpenSession())
            {
                seatClasses = session.Query<SeatClass>().ToList();
            }
            return seatClasses;
        }

        [HttpGet]
        [Route("seat/class/{id}")]
        public IHttpActionResult Get(Int64 id)
        {
            SeatClass seatClass;
            using (ISession session = NHibernateSession.OpenSession())
            {
                seatClass = session.Get<SeatClass>(id);
            }
            if (seatClass == null)
            {
                return NotFound();
            }
            return Ok(seatClass);
        }

        [HttpGet]
        [Route("seat")]
        public IEnumerable<SeatVM> GetSeats()
        {
            IEnumerable<SeatVM> seats;
            using (ISession session = NHibernateSession.OpenSession())
            {
                seats = session.Query<SeatVM>().ToList();
            }
            return seats;
        }

        [HttpGet]
        [Route("seat/plane/{id}")]
        public IHttpActionResult GetByPlane(Int64 id)
        {
            IEnumerable<SeatVM> seats;
            using (ISession session = NHibernateSession.OpenSession())
            {
                seats = session.Query<SeatVM>().Where(p => p.PlaneId == id).ToList();
            }
           
            return Ok(seats);
        }

        [HttpGet]
        [Route("seat/plane/{id}/class/{classId}")]
        public IHttpActionResult GetByPlaneAndClass(Int64 id, Int64 classId)
        {
            IEnumerable<SeatVM> seats;
            using (ISession session = NHibernateSession.OpenSession())
            {
                seats = session.Query<SeatVM>().Where(p => p.PlaneId == id && p.SeatClassId == classId).ToList();
            }

            return Ok(seats);
        }
    }
}
