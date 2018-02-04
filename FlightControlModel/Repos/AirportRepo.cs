using FlightControlApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FlightControlModel.Repos
{
    public class AirportRepo : CRUD
    {
        public AirportRepo(IDbConnection conn, IDbCommand comm) : base(conn, comm)
        {
        }

        public Airport Get(int id)
        {
            Airport a;

            _comm.CommandText = "SELECT * " +
                                "FROM Airport a " +
                                "WHERE a.Id = @id;";

            _comm.AddParameter("@id", id);

            _conn.Open();

            try
            {
                using (IDataReader rdr = _comm.ExecuteReader())
                {
                    rdr.Read();

                    int cid;
                    if ((rdr["CountryId"] == DBNull.Value))
                    {
                        cid = 0;
                    }
                    else
                    {
                        cid = Convert.ToInt32(rdr["CountryId"]);
                    }

                    a = new Airport()
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        Name = Convert.ToString(rdr["Name"]),
                        Address = Convert.ToString(rdr["Address"]),
                        ZipCode = Convert.ToString(rdr["ZipCode"]),
                        CountryId = cid

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
        public List<Airport> GetAll()
        {
            List<int> allAirportsIDs = new List<int>();
            List<Airport> allAirports = new List<Airport>();

            _comm.CommandText = "SELECT * " +
                                "FROM Airport ";

            _conn.Open();

            try
            {
                using (IDataReader rdr = _comm.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        allAirportsIDs.Add(Convert.ToInt32(rdr["Id"]));

                        int cid;
                        if ((rdr["CountryId"] == DBNull.Value))
                        {
                            cid = 0;
                        }
                        else
                        {
                            cid = Convert.ToInt32(rdr["CountryId"]);
                        }

                        allAirports.Add(new Airport()
                        {
                            Id = Convert.ToInt32(rdr["Id"]),
                            Name = Convert.ToString(rdr["Name"]),
                            Address = Convert.ToString(rdr["Address"]),
                            ZipCode = Convert.ToString(rdr["ZipCode"]),
                            CountryId = cid

                        });
                    }
                }

                _conn.Close();

                //foreach (int id in allAirportsIDs)
                //{
                //    allAirports.Add(this.Get(id));
                //}

                return allAirports;
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
        public int Insert(Airport p)
        {
            _comm.CommandText = "INSERT INTO Airport " +
                                "(Name, Address, ZIPCode, CountryId) " +
                                "VALUES " +
                                "(@name, @address, @zipcode, @countryid);" +
                                "SELECT SCOPE_IDENTITY();";

            _comm.AddParameter("@id", p.Id);
            _comm.AddParameter("@name", p.Name);
            _comm.AddParameter("@address", p.Address);
            _comm.AddParameter("@zipcode", p.ZipCode);
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
        public bool Update(int id, Airport p)
        {
            _comm.CommandText = "UPDATE Airport " +
                                "SET  Name = @name, Address = @address, ZIPCode = @zipcode, CountryId = @countryid " +
                                "WHERE Id = @id ";

            _comm.AddParameter("@id", p.Id);
            _comm.AddParameter("@name", p.Name);
            _comm.AddParameter("@address", p.Address);
            _comm.AddParameter("@zipcode", p.ZipCode);
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
            _comm.CommandText = "DELETE FROM Airport " +
                                "WHERE Airport.Id = @id;";

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

