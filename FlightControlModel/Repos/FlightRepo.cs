using FlightControlApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FlightControlModel.Repos
{
    public class FlightRepo : CRUD
    {
        public FlightRepo(IDbConnection conn, IDbCommand comm) : base(conn, comm)
        {
        }

        public Flight Get(int id)
        {
            Flight p;

            _comm.CommandText = "SELECT * " +
                                "FROM Flight p " +
                                "WHERE p.Id = @id;";

            _comm.AddParameter("@id", id);

            _conn.Open();

            try
            {
                using (IDataReader rdr = _comm.ExecuteReader())
                {
                    rdr.Read();

                    p = new Flight()
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        RouteId = Convert.ToInt32(rdr["RouteId"]),
                        PlaneId = Convert.ToInt32(rdr["PlaneId"]),
                        PilotId = Convert.ToInt32(rdr["PilotId"]),
                        DepTime = Convert.ToDateTime(rdr["DepTime"]),
                        ArrTime = Convert.ToDateTime(rdr["ArrTime"]),
                        Canceled = Convert.ToBoolean(rdr["Canceled"]),
                        Price = Convert.ToInt32(rdr["Price"])
                    };

                }

                _conn.Close();

                return p;
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
        public List<Flight> GetAll()
        {
            List<int> allFlightsIDs = new List<int>();
            List<Flight> allFlights = new List<Flight>();

            _comm.CommandText = "SELECT * " +
                                "FROM Flight ";

            _conn.Open();

            try
            {
                using (IDataReader rdr = _comm.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        allFlightsIDs.Add(Convert.ToInt32(rdr["Id"]));

                        allFlights.Add(new Flight()
                        {
                            Id = Convert.ToInt32(rdr["Id"]),
                            RouteId = Convert.ToInt32(rdr["RouteId"]),
                            PlaneId = Convert.ToInt32(rdr["PlaneId"]),
                            PilotId = Convert.ToInt32(rdr["PilotId"]),
                            DepTime = Convert.ToDateTime(rdr["DepTime"]),
                            ArrTime = Convert.ToDateTime(rdr["ArrTime"]),
                            Canceled = Convert.ToBoolean(rdr["Canceled"]),
                            Price = Convert.ToInt32(rdr["Price"])
                        });
                    }
                }

                _conn.Close();

                //foreach (int id in allFlightsIDs)
                //{
                //    allFlights.Add(this.Get(id));
                //}

                return allFlights;
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
        public int Insert(Flight p)
        {
            _comm.CommandText = "INSERT INTO Flight " +
                                "(RouteId, PlaneId, PilotId, DepTime, ArrTime, Canceled, Price) " +
                                "VALUES " +
                                "(@routeid, @planeid, @pilotid, @deptime, @arrtime, @canc, @price);" +
                                "SELECT SCOPE_IDENTITY();";

            _comm.AddParameter("@canc", false);
            _comm.AddParameter("@routeid", p.RouteId);
            _comm.AddParameter("@planeid", p.PlaneId);
            _comm.AddParameter("@pilotid", p.PilotId);
            _comm.AddParameter("@deptime", p.DepTime);
            _comm.AddParameter("@arrtime", p.ArrTime);
            _comm.AddParameter("@price", p.Price);

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
        public bool Update(int id, Flight p)
        {
            _comm.CommandText = "UPDATE Flight " +
                                "SET RouteId = @routeid, PlaneId = @planeid, PilotId = @pilotid, DepTime = @deptime, ArrTime = @arrtime, Price = @price " +
                                "WHERE Id = @id ";

            _comm.AddParameter("@id", p.Id);
            _comm.AddParameter("@routeid", p.RouteId);
            _comm.AddParameter("@planeid", p.PlaneId);
            _comm.AddParameter("@pilotid", p.PilotId);
            _comm.AddParameter("@deptime", p.DepTime);
            _comm.AddParameter("@arrtime", p.ArrTime);
            _comm.AddParameter("@price", p.Price);

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
            _comm.CommandText = "UPDATE Flight " +
                                "SET Canceled = 1" +
                                "WHERE Id = @id ";

            _comm.AddParameter("@id", id);
           

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
    }
}
