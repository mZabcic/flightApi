using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FlightControlModel.Repos
{
    public abstract class CRUD
    {
        protected IDbConnection _conn;
        protected IDbCommand _comm;
        protected IDbTransaction _trans;

        public CRUD(IDbConnection conn, IDbCommand comm)
        {
            _conn = conn;
            _comm = comm;
            _comm.Connection = _conn;
        }
    }
}
