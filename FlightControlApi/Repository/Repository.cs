using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Transaction;

namespace FlightControlApi.Repository
{
    public class Repository<T> : IRepository<T>
    {
        
        protected ISession Session
        {
            get { return NHibernateSession.OpenSession(); }
        }
        

        public T Add(T entity)
        {
            Session.Save(entity);
            Session.Flush();
            return entity;
        }

         public bool Delete(Int64 id)
        {

            using (ISession session = NHibernateSession.OpenSession())
            {
                
                T entity = session.Get<T>(id);
                if (entity == null)
                {
                    return false;
                }
                session.Delete(entity);

                session.Flush();
            }
            return true;
        }

        public bool Update(T entity, Int64 id)
        {

            using (ISession session = NHibernateSession.OpenSession())
            {
                session.Transaction.Begin();
                T old = session.Load<T>(id);
                old = entity;
                session.Update(old);

                try
                {
                    session.Transaction.Commit();
                }
                catch (NHibernate.StaleStateException exception)
                {
                    session.Transaction.Dispose();
                    return false;
                }

            }

            return true;



        }

        public T GetById(Int64 id)
        {
            return Session.Get<T>(id); ;
        }

        public IEnumerable<T> FindAll()
        {

            return Session.Query<T>().ToList();
        }

        public IEnumerable<T> FindByCriteria(NHibernate.Criterion.DetachedCriteria criteria)
        {
            return criteria.GetExecutableCriteria(Session).List<T>();
        }

    }
}
