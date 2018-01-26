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
using FlightControlApi.Repository;
using NHibernate.Criterion;

namespace FlightControlApi.Controllers
{
    public class StoreController : ApiController
    {

        IRepository<Store> repo;
        IRepository<StoreVM> repoVM;

        public StoreController()
        {

            repo = new Repository<Store>();
            repoVM = new Repository<StoreVM>();
        }


        [HttpGet]
        [Route("store")]
        public IEnumerable<StoreVM> Get()
        {
            return repoVM.FindAll();
        }

        [HttpGet]
        [Route("store/{id}")]
        public IHttpActionResult Get(Int64 id)
        {
            StoreVM store = repoVM.GetById(id);
            if (store == null)
            {
                return NotFound();
            }
            return Ok(store);
        }


        [HttpGet]
        [Route("store/country/{id}")]
        public IHttpActionResult GetByCountry(Int64 id)
        {
            var criteria = NHibernate.Criterion.DetachedCriteria.For<StoreVM>()
             .Add(Restrictions.Eq("CountryId", id));

            IEnumerable<StoreVM> stores = repoVM.FindByCriteria(criteria);

            return Ok(stores);
        }

        [HttpPost]
        [Route("store")]
        public IHttpActionResult Post([FromBody]Store store)
        {

            if (store.Name == null)
            {
                return BadRequest("Name is required");
            }
            if (store.Address == null)
            {
                return BadRequest("Address is required");
            }
            if (store.ZipCode == null)
            {
                return BadRequest("ZipCode is required");
            }
            if (store.CountryId == 0)
            {
                return BadRequest("CountryId is required");
            }
            if (!StoreController.CheckCountry(store.CountryId))
            {
                return BadRequest("CountryId is wrong");
            }



            store = repo.Add(store);
            return Ok(store);
        }

        [HttpPut]
        [Route("store/{id}")]
        public IHttpActionResult Put(Int64 id, [FromBody]Store store)
        {
            store.Id = id;
            if (!StoreController.CheckCountry(store.CountryId))
            {
                return BadRequest("CountryId is wrong");
            } 

            bool check = repo.Update(store, id);

            if (check)
                return Ok();
            else
                return NotFound();
        }

        [HttpDelete]
        [Route("store/{id}")]
        public IHttpActionResult Delete(Int64 id)
        {

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

   


    }
}
