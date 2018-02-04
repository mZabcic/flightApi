using FlightControlApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FlightControlModel.Repos
{
    public class SeatRepo : CRUD
    {
        public SeatRepo(IDbConnection conn, IDbCommand comm) : base(conn, comm)
        {
        }

        public Seat Get(int id)
        {
            Seat a;

            _comm.CommandText = "SELECT * " +
                                "FROM Seat a " +
                                "WHERE a.Id = @id;";

            _comm.AddParameter("@id", id);

            _conn.Open();

            try
            {
                using (IDataReader rdr = _comm.ExecuteReader())
                {
                    rdr.Read();

                    a = new Seat()
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        Num = Convert.ToInt32(rdr["Num"]),
                        PlaneId = Convert.ToInt32(rdr["PlaneId"]),
                        SeatClassId = Convert.ToInt32(rdr["SeatClassId"]),

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
        public List<Seat> GetAll()
        {
            List<int> allSeatsIDs = new List<int>();
            List<Seat> allSeats = new List<Seat>();

            _comm.CommandText = "SELECT * " +
                                "FROM Seat ";

            _conn.Open();

            try
            {
                using (IDataReader rdr = _comm.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        allSeatsIDs.Add(Convert.ToInt32(rdr["Id"]));

                        allSeats.Add(new Seat()
                        {
                            Id = Convert.ToInt32(rdr["Id"]),
                            Num = Convert.ToInt32(rdr["Num"]),
                            PlaneId = Convert.ToInt32(rdr["PlaneId"]),
                            SeatClassId = Convert.ToInt32(rdr["SeatClassId"]),

                        });
                    }
                }

                _conn.Close();

                //foreach (int id in allSeatsIDs)
                //{
                //    allSeats.Add(this.Get(id));
                //}

                return allSeats;
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
        public int Insert(Seat p)
        {
            _comm.CommandText = "INSERT INTO Seat " +
                                "(Num, PlaneId, SeatClassId) " +
                                "VALUES " +
                                "(@num, @planeid, @seatclassid);" +
                                "SELECT SCOPE_IDENTITY();";

            _comm.AddParameter("@id", p.Id);
            _comm.AddParameter("@num", p.Num);
            _comm.AddParameter("@planeid", p.PlaneId);
            _comm.AddParameter("@seatclassid", p.SeatClassId);

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
        public bool Update(int id, Seat p)
        {
            _comm.CommandText = "UPDATE Seat " +
                                "SET Num = @num, PlaneId = @planeid, SeatClassId = @seatclassid" +
                                "WHERE Id = @id ";

            _comm.AddParameter("@id", p.Id);
            _comm.AddParameter("@num", p.Num);
            _comm.AddParameter("@planeid", p.PlaneId);
            _comm.AddParameter("@seatclassid", p.SeatClassId);

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
            _comm.CommandText = "DELETE FROM Seat " +
                                "WHERE Seat.Id = @id;";

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

