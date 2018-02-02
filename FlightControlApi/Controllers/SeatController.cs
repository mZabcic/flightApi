using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlightControlApi.Models;
using NHibernate;
using NHibernate.Linq;
using FlightControlApi.Repository;
using NHibernate.Criterion;

namespace FlightControlApi.Controllers
{
    public class SeatController : ApiController
    {

        IRepository<Seat> repo;
        IRepository<SeatClass> classRepo;

        public SeatController()
        {
            repo = new Repository<Seat>();
            classRepo = new Repository<SeatClass>();
        }


        [HttpGet]
        [Route("seat/class")]
        public IEnumerable<SeatClass> Get()
        {
            IEnumerable<SeatClass> seatClasses = classRepo.FindAll();
            
            return seatClasses;
        }

        [HttpGet]
        [Route("seat/class/{id}")]
        public IHttpActionResult Get(Int64 id)
        {
            SeatClass seatClass = classRepo.GetById(id);
            
            if (seatClass == null)
            {
                return NotFound();
            }
            return Ok(seatClass);
        }

        [HttpGet]
        [Route("seat")]
        public IEnumerable<Seat> GetSeats()
        {
            IEnumerable<Seat> seats = repo.FindAll();
            
            return seats;
        }

        [HttpGet]
        [Route("seat/plane/{id}")]
        public IHttpActionResult GetByPlane(Int64 id)
        {
            var criteria = NHibernate.Criterion.DetachedCriteria.For<Seat>()
            .Add(Restrictions.Eq("PlaneId", id));
            IEnumerable<Seat> seats = repo.FindByCriteria(criteria);
            
           
            return Ok(seats);
        }

        [HttpGet]
        [Route("seat/plane/{id}/class/{classId}")]
        public IHttpActionResult GetByPlaneAndClass(Int64 id, Int64 classId)
        {
            var criteria = NHibernate.Criterion.DetachedCriteria.For<Seat>()
           .Add(Restrictions.Eq("PlaneId", id)).Add(Restrictions.Eq("SeatClassId", classId));
            IEnumerable<Seat> seats = repo.FindByCriteria(criteria);
            

            return Ok(seats);
        }
    }
}
