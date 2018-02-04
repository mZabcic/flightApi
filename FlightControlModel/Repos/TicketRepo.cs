using FlightControlApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FlightControlModel.Repos
{
    public class TicketRepo : CRUD
    {
        public TicketRepo(IDbConnection conn, IDbCommand comm) : base(conn, comm)
        {
        }

        public Ticket Get(int id)
        {
            Ticket a;

            _comm.CommandText = "SELECT * " +
                                "FROM Ticket a " +
                                "WHERE a.Id = @id;";

            _comm.AddParameter("@id", id);

            _conn.Open();

            try
            {
                using (IDataReader rdr = _comm.ExecuteReader())
                {
                    rdr.Read();

                    a = new Ticket()
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        Price = Convert.ToInt64(rdr["Price"]),
                        PassengerId = Convert.ToInt64(rdr["PassengerId"]),
                        SeatId = Convert.ToInt64(rdr["SeatId"]),
                        StoreId = Convert.ToInt32(rdr["StoreId"]),
                        FlightId = Convert.ToInt64(rdr["FlightId"]),
                        Revoked = Convert.ToBoolean(rdr["FlightId"])

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
        public List<Ticket> GetAll()
        {
            List<int> allTicketsIDs = new List<int>();
            List<Ticket> allTickets = new List<Ticket>();

            _comm.CommandText = "SELECT * " +
                                "FROM Ticket ";

            _conn.Open();

            try
            {
                using (IDataReader rdr = _comm.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        allTicketsIDs.Add(Convert.ToInt32(rdr["Id"]));

                        allTickets.Add(new Ticket()
                        {
                            Id = Convert.ToInt32(rdr["Id"]),
                            Price = Convert.ToInt64(rdr["Price"]),
                            PassengerId = Convert.ToInt64(rdr["PassengerId"]),
                            SeatId = Convert.ToInt64(rdr["SeatId"]),
                            StoreId = Convert.ToInt32(rdr["StoreId"]),
                            FlightId = Convert.ToInt64(rdr["FlightId"]),
                            Revoked = Convert.ToBoolean(rdr["FlightId"])

                        });
                    }
                }

                _conn.Close();

                //foreach (int id in allTicketsIDs)
                //{
                //    allTickets.Add(this.Get(id));
                //}

                return allTickets;
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
        public int Insert(Ticket p)
        {
            _comm.CommandText = "INSERT INTO Ticket " +
                                "(Price, PassengerId, SeatId, StoreId, FlightId) " +
                                "VALUES " +
                                "(@price, @passid, @seatid, @storeid, @flightid);" +
                                "SELECT SCOPE_IDENTITY();";

            _comm.AddParameter("@price", p.Price);
            _comm.AddParameter("@passid", p.PassengerId);
            _comm.AddParameter("@seatid", p.SeatId);
            _comm.AddParameter("@storeid", p.StoreId);
            _comm.AddParameter("@flightid", p.FlightId);

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
        public bool Update(int id, Ticket p)
        {
            _comm.CommandText = "UPDATE Ticket " +
                                "SET Price = @price, PassengerId = @passid, SeatId = @seatid, StoreId = @storeid, FlightId = @flightid " +
                                "WHERE Id = @id ";

            _comm.AddParameter("@id", p.Id);
            _comm.AddParameter("@price", p.Price);
            _comm.AddParameter("@passid", p.PassengerId);
            _comm.AddParameter("@seatid", p.SeatId);
            _comm.AddParameter("@storeid", p.StoreId);
            _comm.AddParameter("@flightid", p.FlightId);
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
            _comm.CommandText = "UPDATE Ticket " +
                               "SET Revoked = 1 " +
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

