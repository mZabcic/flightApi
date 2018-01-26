using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightControlApi.Repository
{
    public interface IRepository<T>
    {
        T Add(T entity);
        bool Delete(Int64 id);
        bool Update(T entity, Int64 id);
        T GetById(Int64 id);
        IEnumerable<T> FindAll();
        IEnumerable<T> FindByCriteria(NHibernate.Criterion.DetachedCriteria criteria);


}
}
