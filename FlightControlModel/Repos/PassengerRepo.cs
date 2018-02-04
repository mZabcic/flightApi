using FlightControlApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FlightControlModel.Repos
{
    public class PassengerRepo : CRUD
    {
        public PassengerRepo(IDbConnection conn, IDbCommand comm) : base(conn, comm)
        {
        }

        public Passenger Get(int id)
        {
            Passenger a;

            _comm.CommandText = "SELECT * " +
                                "FROM Passenger a " +
                                "WHERE a.Id = @id;";

            _comm.AddParameter("@id", id);

            _conn.Open();

            try
            {
                using (IDataReader rdr = _comm.ExecuteReader())
                {
                    rdr.Read();

                    a = new Passenger()
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        Name = Convert.ToString(rdr["Name"]),
                        Email = Convert.ToString(rdr["Email"]),
                        Identifier = Convert.ToString(rdr["Identifier"]),
                        CountryId = Convert.ToInt32(rdr["CountryId"])

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
        public List<Passenger> GetAll()
        {
            List<int> allPassengersIDs = new List<int>();
            List<Passenger> allPassengers = new List<Passenger>();

            _comm.CommandText = "SELECT * " +
                                "FROM Passenger ";

            _conn.Open();

            try
            {
                using (IDataReader rdr = _comm.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        allPassengersIDs.Add(Convert.ToInt32(rdr["Id"]));

                        allPassengers.Add(new Passenger()
                        {
                            Id = Convert.ToInt32(rdr["Id"]),
                            Name = Convert.ToString(rdr["Name"]),
                            Email = Convert.ToString(rdr["Email"]),
                            Identifier = Convert.ToString(rdr["Identifier"]),
                            CountryId = Convert.ToInt32(rdr["CountryId"])

                        });
                    }
                }

                _conn.Close();

                //foreach (int id in allPassengersIDs)
                //{
                //    allPassengers.Add(this.Get(id));
                //}

                return allPassengers;
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
        public int Insert(Passenger p)
        {
            _comm.CommandText = "INSERT INTO Passenger " +
                                "(Name, Email, Identifier, CountryId) " +
                                "VALUES " +
                                "(@name, @email, @identifier, @countryid);" +
                                "SELECT SCOPE_IDENTITY();";

            _comm.AddParameter("@id", p.Id);
            _comm.AddParameter("@name", p.Name);
            _comm.AddParameter("@email", p.Email);
            _comm.AddParameter("@identifier", p.Identifier);
            _comm.AddParameter("@countryid", p.CountryId);

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
        public bool Update(int id, Passenger p)
        {
            _comm.CommandText = "UPDATE Passenger " +
                                "SET Name = @name, Email = @email, Identifier = @identifier, CountryId = @countryid " +
                                "WHERE Id = @id ";

            _comm.AddParameter("@id", p.Id);
            _comm.AddParameter("@name", p.Name);
            _comm.AddParameter("@email", p.Email);
            _comm.AddParameter("@Identifier", p.Identifier);
            _comm.AddParameter("@countryid", p.CountryId);

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
            _comm.CommandText = "DELETE FROM Passenger " +
                                "WHERE Passenger.Id = @id;";

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

