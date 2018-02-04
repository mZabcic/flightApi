using FlightControlApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FlightControlModel.Repos
{
    public class RouteRepo : CRUD
    {
        public RouteRepo(IDbConnection conn, IDbCommand comm) : base(conn, comm)
        {
        }

        public Route Get(int id)
        {
            Route a;

            _comm.CommandText = "SELECT * " +
                                "FROM Route a " +
                                "WHERE a.Id = @id;";

            _comm.AddParameter("@id", id);

            _conn.Open();

            try
            {
                using (IDataReader rdr = _comm.ExecuteReader())
                {
                    rdr.Read();

                    a = new Route()
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        FromId = Convert.ToInt32(rdr["FromId"]),
                        DestinationId = Convert.ToInt32(rdr["DestinationId"])

                    };

                }

                _conn.Close();

                return a;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _comm.Parameters.Clear();
                _conn.Close();
            }


        }
        public List<Route> GetAll()
        {
            List<int> allRoutesIDs = new List<int>();
            List<Route> allRoutes = new List<Route>();

            _comm.CommandText = "SELECT * " +
                                "FROM Route ";

            _conn.Open();

            try
            {
                using (IDataReader rdr = _comm.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        allRoutesIDs.Add(Convert.ToInt32(rdr["Id"]));

                        allRoutes.Add(new Route()
                        {
                            Id = Convert.ToInt32(rdr["Id"]),
                            FromId = Convert.ToInt32(rdr["FromId"]),
                            DestinationId = Convert.ToInt32(rdr["DestinationId"])

                        });
                    }
                }

                _conn.Close();

                //foreach (int id in allRoutesIDs)
                //{
                //    allRoutes.Add(this.Get(id));
                //}

                return allRoutes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _conn.Close();
            }
        }
        public int Insert(Route p)
        {
            _comm.CommandText = "INSERT INTO Route " +
                                "(FromId, DestinationId) " +
                                "VALUES " +
                                "(@from, @to);" +
                                "SELECT SCOPE_IDENTITY();";

            _comm.AddParameter("@id", p.Id);
            _comm.AddParameter("@from", p.FromId);
            _comm.AddParameter("@to", p.DestinationId);

            _conn.Open();

            try
            {
                int recordID = Convert.ToInt32(_comm.ExecuteScalar());

                _comm.Parameters.Clear();
                _conn.Close();

                return recordID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _comm.Parameters.Clear();
                _conn.Close();
            }
        }
        public bool Update(int id, Route p)
        {
            _comm.CommandText = "UPDATE Route " +
                                "SET FromId = @from, DestinationId = @to " +
                                "WHERE Id = @id ";

            _comm.AddParameter("@id", p.Id);
            _comm.AddParameter("@from", p.FromId);
            _comm.AddParameter("@to", p.DestinationId);

            _conn.Open();

            try
            {
                _comm.ExecuteNonQuery();

                _comm.Parameters.Clear();
                _conn.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _comm.Parameters.Clear();
                _conn.Close();
            }
        }
        public bool Delete(int id)
        {
            _comm.CommandText = "DELETE FROM Route " +
                                "WHERE Route.Id = @id;";

            _comm.AddParameter("@id", id);

            _conn.Open();

            _trans = _conn.BeginTransaction();

            _comm.Transaction = _trans;

            try
            {
                _comm.ExecuteNonQuery();
                _trans.Commit();
            }
            catch (Exception ex)
            {
                _trans.Rollback();
                throw ex;
            }
            finally
            {
                _comm.Transaction = null;
                _comm.Parameters.Clear();
                _conn.Close();
            }

            return true;
        }
    }
}

