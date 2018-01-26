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

namespace FlightControlApi.Controllers
{
    public class CountryController : ApiController
    {

        IRepository<Country> repo;
   

        public CountryController()
        {

            repo = new Repository<Country>();
        }


        [HttpGet]
        [Route("country")]
        public IEnumerable<Country> Get()
        {
            IEnumerable<Country> countries = repo.FindAll();
            
            return countries;
        }

        [HttpGet]
        [Route("country/{id}")]
        public IHttpActionResult Get(Int64 id)
        {
            Country countries = repo.GetById(id);
            
            if (countries == null)
            {
                return NotFound();
            }
            return Ok(countries);
        }
    }
}
