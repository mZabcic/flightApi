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
    public class PilotController : ApiController
    {

        IRepository<Pilot> repo;

        public PilotController()
        {

            repo = new Repository<Pilot>();
        }


        [HttpGet]
        [Route("pilot")]
        public IEnumerable<Pilot> Get()
        {
            IEnumerable<Pilot> pilots;

            pilots = repo.FindAll();
            
            return pilots;
        }

        [HttpGet]
        [Route("pilot/{id}")]
        public IHttpActionResult Get(Int64 id)
        {
            Pilot pilot = repo.GetById(id);
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

            Pilot newPilot = repo.Add(pilot);
            return Ok(pilot);
        }

        [HttpPut]
        [Route("pilot/{id}")]
        public IHttpActionResult Put(Int64 id, [FromBody]Pilot pilot)
        {
          
            pilot.Id = id;
            pilot.Active = true;

            bool check = repo.Update(pilot, id);

            if (check)
                return Ok();
            else
                return NotFound();


        }

        [HttpDelete]
        [Route("pilot/{id}")]
        public IHttpActionResult Delete(Int64 id)
        {
            Pilot pilot = repo.GetById(id);
            pilot.Active = false;
            bool check = repo.Update(pilot, id);

            if (check)
                return Ok();
            else
                return NotFound();

        }

       
    }
}
