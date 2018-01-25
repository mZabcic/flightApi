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

namespace FlightControlApi.Controllers
{
    public class StoreController : ApiController
    {


        [HttpGet]
        [Route("store")]
        public IEnumerable<StoreVM> Get()
        {
            IEnumerable<StoreVM> stores;
            using (ISession session = NHibernateSession.OpenSession())
            {
                stores = session.Query<StoreVM>().ToList();
            }

            return stores;
        }

        [HttpGet]
        [Route("store/{id}")]
        public IHttpActionResult Get(Int64 id)
        {
            StoreVM store;
            using (ISession session = NHibernateSession.OpenSession())
            {
                store = session.Get<StoreVM>(id);
            }
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
            IEnumerable<StoreVM> stores;
            using (ISession session = NHibernateSession.OpenSession())
            {
                stores = session.Query<StoreVM>().Where(p => p.CountryId == id).ToList();
            }

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



            using (ISession session = NHibernateSession.OpenSession())
            {
                session.Save(store);
            }
            return Ok(store);
        }

        [HttpPut]
        [Route("store/{id}")]
        public IHttpActionResult Put(Int64 id, [FromBody]Store store)
        {
            Store oldStore;
            store.Id = id;
            if (!StoreController.CheckCountry(store.CountryId))
            {
                return BadRequest("CountryId is wrong");
            }


            using (ISession session = NHibernateSession.OpenSession())
            {
                session.Transaction.Begin();
                oldStore = session.Load<Store>(id);
                oldStore = store;
                session.Update(oldStore);

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


            return Ok(oldStore);
        }

        [HttpDelete]
        [Route("store/{id}")]
        public IHttpActionResult Delete(Int64 id)
        {

            using (ISession session = NHibernateSession.OpenSession())
            {
                int count = session.Query<Ticket>().Where(c => c.StoreId == id).Count();
                if (count > 0)
                {
                    return Conflict();
                } 
                Store store = session.Get<Store>(id);
                if (store == null)
                {
                    return NotFound();
                }
                session.Delete(store);

                session.Flush();
            }
            return Ok();

        }

        private static bool CheckCountry(Int64 id)
        {
            int count;
            using (ISession session = NHibernateSession.OpenSession())
            {
                count = session.Query<Country>().Where(p => p.Id == id).Count();
            }
            return count > 0;
        }

   


    }
}
